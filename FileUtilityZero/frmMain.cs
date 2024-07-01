using ClosedXML.Excel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FileUtilityZero
{
    public partial class FrmMain : Form
    {
        // Set the log file path
        public static string LogDirectory = @"C:\File Utility Zero\log.txt";

        public static string FUZDirectory = @"C:\File Utility Zero";

        // Set the working path
        private string WorkingPath = string.Empty;

        // Set the timer count (number of seconds the scan has been running)
        private int timerCount = 0;

        public static int tick = 0;

        private FileAccess FileInfo = new();

        public static int FileCount = 0;

        public FrmMain()
        {
            InitializeComponent();
        }

        public static void StatusTick()
        {
            switch (tick)
            {
                case 0:
                    lblStatus.Text = "Status: Scanning files";
                    tick++;
                    break;
                case 1:
                    lblStatus.Text = "Status: Scanning files.";
                    tick++;
                    break;
                case 2:
                    lblStatus.Text = "Status: Scanning files..";
                    tick++;
                    break;
                case 3:
                    lblStatus.Text = "Status: Scanning files...";
                    tick++;
                    break;
                case 4:
                    lblStatus.Text = "Status: Scanning files....";
                    tick++;
                    break;
                case 5:
                    lblStatus.Text = "Status: Scanning files.....";
                    tick = 0;
                    break;
                default:
                    lblStatus.Text = "Status: Scanning files";
                    tick = 0;
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

            btnRun.Enabled = false;

            DateTime currentDateTime = DateTime.Now;
            StreamWriter streamWriter = new(FUZDirectory + @"\files_auto_" + currentDateTime.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv", true);

            txtOutput.Text = "Scanning files in the Working Path into a data table. This will take some time if there is a large number of files to be scanned. Please be patient.";
            lblStatus.Text = "Status: Working...";

            // Get all files last access info
            int result = FileAccess.GetFiles(WorkingPath);
            lblFileTotal.Text = "Total number of files: " + result.ToString();

            string line = "File Name, File Path, File Size, Creation Time, Last Write Time, Last Access Time";
            streamWriter.WriteLine(line);
            streamWriter.Flush();

            // Display the file access info
            if (result > 0)
            {
                foreach (DataRow row in FileAccess.filesDT.Rows)
                {
                    FileAccessInfo file_Info = new()
                    {
                        FileName = row[0].ToString(),
                        LastAccessTime = row[1].ToString(),
                        FilePath = row[2].ToString(),
                        FileSize = row[3].ToString(),
                        CreationTime = row[4].ToString(),
                        LastWriteTime = row[5].ToString()
                    };

                    string currentFileInfo = ($"File Name: {file_Info.FileName}, File Path: {file_Info.FilePath}, " +
                        $"File Size: {file_Info.FileSize}, Creation Time: {file_Info.CreationTime}, " +
                        $"Last Write Time: {file_Info.LastWriteTime}, Last Access Time: {file_Info.LastAccessTime}");

                    FileCount++;
                    lblFileCount.Text = "Number of files scanned: " + FileCount.ToString();

                    txtOutput.Text = (currentFileInfo);
                    
                    // Append the file info to the auto generated CSV file
                    string thisInfo = ($"{file_Info.FileName}, {file_Info.FilePath}, {file_Info.FileSize}, {file_Info.CreationTime}, {file_Info.LastWriteTime}, {file_Info.LastAccessTime}");
                    streamWriter.WriteLine(thisInfo);
                    streamWriter.Flush();
                    streamWriter.Close();

                    StatusTick();
                }

                btnExport.Enabled = true;
                lblStatus.Text = "Status: Scanning complete.";
                txtOutput.Text = "Scanning complete.";
            }
            else
            {
                txtOutput.Text = "No files found in the Working Path.";
                Logger.Log("No files found in the Working Path.");
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
            string CSVFilePath = FUZDirectory + @"\files_export_" + currentDateTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";

            try
            {
                FileAccess.ExportDataTableToCSV(FileAccess.filesDT, CSVFilePath);
                btnExport.Enabled = false;
                MessageBox.Show("The data has been exported to " + CSVFilePath, "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Log($"An error occurred while exporting the data to CSV: {ex.Message}");
            }
        }
    }
}
