using System.Reflection;
using FileUtilityZero.Core;

namespace FileUtilityZero
{
    public partial class FrmMain : Form
    {
        // Cached once since FileScanResult's shape never changes at runtime -
        // used by both the free-text filter (checks every column) and the
        // column-sort handler (looks up the clicked column's property).
        private static readonly PropertyInfo[] ResultProperties = typeof(FileScanResult).GetProperties();

        // Output/log locations - same literal values as the old static
        // FUZDirectory/LogDirectory fields, just instance-scoped now.
        private readonly string _outputDirectory = @"C:\File Utility Zero";
        private readonly string _logFilePath = @"C:\File Utility Zero\log.txt";

        private readonly ILogger _logger;
        private readonly IFileSystem _fileSystem;
        private readonly FileScanner _scanner;
        private readonly CsvExporter _csvExporter;

        // Set the working path
        private string WorkingPath = string.Empty;

        // Set the timer count (number of seconds the scan has been running)
        private int timerCount = 0;

        private int _fileCount = 0;

        // True while a scan is running on the background thread. Guards the
        // filter box and column-sort handlers so they don't try to
        // re-filter/re-sort against a result set that's still being built.
        private bool _isScanning;

        // Results of the most recent scan, used by the Export csv button.
        // This is always the full, unfiltered set - dgvResults is bound to a
        // separately-computed filtered/sorted view (see RefreshGrid), so
        // Export csv always exports everything regardless of what the user
        // has currently typed into the search box.
        private List<FileScanResult> _scanResults = new();

        private readonly BindingSource _resultsBindingSource = new();

        // Which column dgvResults is currently sorted by (a FileScanResult
        // property name), and in which direction. Null means unsorted
        // (results shown in scan order).
        private string? _sortProperty;
        private bool _sortAscending = true;

        public FrmMain()
        {
            InitializeComponent();

            _logger = new FileLogger(_logFilePath);
            _fileSystem = new FileSystem();
            _scanner = new FileScanner(_fileSystem, _logger);
            _csvExporter = new CsvExporter();

            dgvResults.DataSource = _resultsBindingSource;
        }

        // Recomputes the grid's data source from _scanResults, applying the
        // current search-box text and column sort. Called after a scan
        // completes and whenever the filter text or sort column changes.
        private void RefreshGrid()
        {
            IEnumerable<FileScanResult> view = _scanResults;

            string filterText = txtFilter.Text;
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                view = view.Where(result => RowMatchesFilter(result, filterText));
            }

            if (_sortProperty != null)
            {
                PropertyInfo property = typeof(FileScanResult).GetProperty(_sortProperty)!;

                // Sorting by the raw property value (rather than its string
                // representation) is what makes File Size sort numerically
                // and the date columns sort chronologically instead of
                // alphabetically - eg "9" would otherwise sort after "10".
                view = _sortAscending
                    ? view.OrderBy(result => property.GetValue(result))
                    : view.OrderByDescending(result => property.GetValue(result));
            }

            _resultsBindingSource.DataSource = view.ToList();
        }

        // True if any column of the result, converted to text, contains
        // filterText (case-insensitive).
        private static bool RowMatchesFilter(FileScanResult result, string filterText)
        {
            foreach (PropertyInfo property in ResultProperties)
            {
                string? value = property.GetValue(result)?.ToString();
                if (value != null && value.Contains(filterText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            if (_isScanning)
            {
                return;
            }

            RefreshGrid();
        }

        private void DgvResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_isScanning)
            {
                return;
            }

            DataGridViewColumn column = dgvResults.Columns[e.ColumnIndex];
            string propertyName = column.DataPropertyName;
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            if (_sortProperty == propertyName)
            {
                _sortAscending = !_sortAscending;
            }
            else
            {
                _sortProperty = propertyName;
                _sortAscending = true;
            }

            foreach (DataGridViewColumn otherColumn in dgvResults.Columns)
            {
                otherColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            column.HeaderCell.SortGlyphDirection = _sortAscending ? SortOrder.Ascending : SortOrder.Descending;

            RefreshGrid();
        }

        // Enables/disables the controls that shouldn't be touched while a
        // scan is in flight on the background thread.
        private void SetControlsEnabled(bool enabled)
        {
            btnRun.Enabled = enabled;
            txtWorkingPath.Enabled = enabled;
            chkIncludeHash.Enabled = enabled;
            chkIncludeCategory.Enabled = enabled;
            txtFilter.Enabled = enabled;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            btnRun.Enabled = true;
            lblStatus.Text = "Status: idle";
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            // Open the Folder Browser Dialog
            DialogResult Result = folderBrowserDialog1.ShowDialog();

            // Set the Working Path if a folder is selected
            if (Result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                WorkingPath = folderBrowserDialog1.SelectedPath;
                txtWorkingPath.Text = WorkingPath;
                btnBrowse.Enabled = false;
            }
        }

        private async void BtnRun_Click(object sender, EventArgs e)
        {
            WorkingPath = txtWorkingPath.Text;

            // Ensure the Working Path is set
            if (string.IsNullOrWhiteSpace(WorkingPath))
            {
                MessageBox.Show("Please select the Working Path option.", "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Reject UNC paths (\\host\share): scanning one causes Windows to attempt
            // SMB authentication against that host automatically, which a rogue SMB
            // listener could capture. Local drives and mapped drive letters are unaffected.
            if (WorkingPathValidator.IsUncPath(WorkingPath))
            {
                MessageBox.Show("Network (UNC) paths are not supported, since scanning one can trigger an automatic network sign-in attempt against the remote host. Please select a local or mapped drive path instead.", "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isScanning = true;
            SetControlsEnabled(false);

            try
            {
                // Reset the per-scan file count so it doesn't carry over from a previous run.
                _fileCount = 0;
                lblFileCount.Text = "Number of files scanned: 0";

                // Start each new scan with a clean grid view rather than carrying
                // over a search/sort left from a previous run's results.
                txtFilter.Text = string.Empty;
                _sortProperty = null;
                foreach (DataGridViewColumn column in dgvResults.Columns)
                {
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                // Ensure the output directory exists before Export csv needs to write into it later.
                if (!_fileSystem.DirectoryExists(_outputDirectory))
                {
                    try
                    {
                        _fileSystem.CreateDirectory(_outputDirectory);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Unable to create output directory '{_outputDirectory}': {ex.Message}");
                        MessageBox.Show($"Could not create the output directory '{_outputDirectory}'.\n\n{ex.Message}", "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // File hashing reads the full contents of every file, so a scan
                // with it enabled is meaningfully slower than a metadata-only
                // scan on a large tree - worth calling out up front since there's
                // no percentage-based progress indicator to reveal that cost
                // once the scan is under way.
                lblStatus.Text = chkIncludeHash.Checked
                    ? "Status: Working... (file hashing enabled, this will be slower)"
                    : "Status: Working...";
                progressBar.Visible = true;

                ScanOptions scanOptions = new(IncludeHash: chkIncludeHash.Checked, IncludeCategory: chkIncludeCategory.Checked);

                // Bind the grid to a fresh, empty list up front, then add each
                // result to it as it's reported - this is what lets the grid
                // populate progressively while the scan runs, rather than
                // waiting for the whole tree to finish. BindingSource.Add
                // raises a single-item ListChanged notification, so
                // DataGridView adds one row at a time instead of redrawing
                // the whole grid per file.
                _resultsBindingSource.DataSource = new List<FileScanResult>();

                IProgress<FileScanResult> progress = new Progress<FileScanResult>(result =>
                {
                    _resultsBindingSource.Add(result);
                    _fileCount++;
                    lblFileCount.Text = "Number of files scanned: " + _fileCount.ToString();
                });

                // Run the scan on a thread pool thread so the UI thread - and
                // with it the marquee progress bar's animation and the
                // progressive grid updates above - stays responsive for the
                // full duration of a large scan.
                _scanResults = await Task.Run(() => _scanner.Scan(WorkingPath, scanOptions, progress));

                lblFileTotal.Text = "Total number of files: " + _scanResults.Count.ToString();

                // The progressive updates above never applied the search box
                // or column sort (both are disabled/ignored while scanning),
                // so reconcile the grid against the final results now that
                // both are live again.
                RefreshGrid();

                if (_scanResults.Count > 0)
                {
                    btnExport.Enabled = true;
                    lblStatus.Text = "Status: Scanning complete.";
                }
                else
                {
                    _logger.Log("No files found in the Working Path.");
                    lblStatus.Text = "Status: No files found in the Working Path.";
                }
            }
            finally
            {
                progressBar.Visible = false;
                _isScanning = false;
                SetControlsEnabled(true);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            // Set the CSV file path
            //
            // Get current date and time
            DateTime currentDateTime = DateTime.Now;
            string CSVFilePath = _outputDirectory + @"\files_export_" + currentDateTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";

            try
            {
                _csvExporter.Export(_scanResults, CSVFilePath);
                btnExport.Enabled = false;
                MessageBox.Show("The data has been exported to " + CSVFilePath, "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Log($"An error occurred while exporting the data to CSV: {ex.Message}");
            }
        }
    }
}
