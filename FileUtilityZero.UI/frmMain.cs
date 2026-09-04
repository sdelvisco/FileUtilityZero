using FileUtilityZero.Core;

namespace FileUtilityZero
{
    public partial class FrmMain : Form
    {
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

        private int _tick = 0;

        private int _fileCount = 0;

        // Results of the most recent scan, used by the Export csv button.
        private List<FileScanResult> _scanResults = new();

        public FrmMain()
        {
            InitializeComponent();

            _logger = new FileLogger(_logFilePath);
            _fileSystem = new FileSystem();
            _scanner = new FileScanner(_fileSystem, _logger);
            _csvExporter = new CsvExporter();
        }

        private void StatusTick()
        {
            switch (_tick)
            {
                case 0:
                    lblStatus.Text = "Status: Scanning files";
                    _tick++;
                    break;
                case 1:
                    lblStatus.Text = "Status: Scanning files.";
                    _tick++;
                    break;
                case 2:
                    lblStatus.Text = "Status: Scanning files..";
                    _tick++;
                    break;
                case 3:
                    lblStatus.Text = "Status: Scanning files...";
                    _tick++;
                    break;
                case 4:
                    lblStatus.Text = "Status: Scanning files....";
                    _tick++;
                    break;
                case 5:
                    lblStatus.Text = "Status: Scanning files.....";
                    _tick = 0;
                    break;
                default:
                    lblStatus.Text = "Status: Scanning files";
                    _tick = 0;
                    break;
            }

            Application.DoEvents();
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

        private void BtnRun_Click(object sender, EventArgs e)
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

            btnRun.Enabled = false;

            // Reset the per-scan file count so it doesn't carry over from a previous run.
            _fileCount = 0;
            lblFileCount.Text = "Number of files scanned: 0";

            // Ensure the output directory exists before attempting to create the CSV file in it.
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
                    btnRun.Enabled = true;
                    return;
                }
            }

            DateTime currentDateTime = DateTime.Now;
            using StreamWriter streamWriter = new(_outputDirectory + @"\files_auto_" + currentDateTime.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv", true);

            // File hashing reads the full contents of every file, so a scan
            // with it enabled is meaningfully slower than a metadata-only
            // scan on a large tree. The animated "Status: Scanning files..."
            // label (see StatusTick) is left alone since it's already
            // ticking for the whole scan duration either way and has no
            // room to say more, but the one-time working message the user
            // sees when a scan starts is a good place to set the
            // expectation up front.
            string workingMessage = "Scanning files in the Working Path into a data table. This will take some time if there is a large number of files to be scanned. Please be patient.";
            if (chkIncludeHash.Checked)
            {
                workingMessage += " File hashing is enabled, which reads the full contents of every file and will make this noticeably slower.";
            }

            txtOutput.Text = workingMessage;
            lblStatus.Text = "Status: Working...";

            ScanOptions scanOptions = new(IncludeHash: chkIncludeHash.Checked, IncludeCategory: chkIncludeCategory.Checked);

            // Get all files last access info
            _scanResults = _scanner.Scan(WorkingPath, scanOptions);
            lblFileTotal.Text = "Total number of files: " + _scanResults.Count.ToString();

            streamWriter.WriteLine(_csvExporter.BuildHeaderLine());
            streamWriter.Flush();

            // Display the file access info
            if (_scanResults.Count > 0)
            {
                foreach (FileScanResult result in _scanResults)
                {
                    string currentFileInfo = ($"File Name: {result.FileName}, File Path: {result.FilePath}, " +
                        $"File Size: {result.FileSize}, Creation Time: {result.CreationTime}, " +
                        $"Last Write Time: {result.LastWriteTime}, Last Access Time: {result.LastAccessTime}");

                    _fileCount++;
                    lblFileCount.Text = "Number of files scanned: " + _fileCount.ToString();

                    txtOutput.Text = (currentFileInfo);

                    // Append the file info to the auto generated CSV file
                    streamWriter.WriteLine(_csvExporter.BuildLine(result));
                    streamWriter.Flush();

                    StatusTick();
                }

                btnExport.Enabled = true;
                lblStatus.Text = "Status: Scanning complete.";
                txtOutput.Text = "Scanning complete.";
            }
            else
            {
                txtOutput.Text = "No files found in the Working Path.";
                _logger.Log("No files found in the Working Path.");
                lblStatus.Text = "Status: idle";
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
