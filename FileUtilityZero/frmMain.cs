using ClosedXML.Excel;
using System.Data;
using System.Runtime.CompilerServices;

namespace FileUtilityZero
{
    public partial class FrmMain : Form
    {
        // Set the log file path
        public static string LogDirectory = @"C:\File Utility Zero\log.txt";

        public static string FUZDirectory = @"C:\File Utility Zero";

        // Set the working path
        private string WorkingPath = string.Empty;

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
            btnView.Enabled = false;
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

            txtOutput.Clear();
            txtOutput.AppendText("Scanning files in the Working Path. This will take some time if there are a large number of files to be scanned." + Environment.NewLine);

            // TODO: Need to put FileAccess.GetAllFiles() in a thread so it doesn't seem like the application stops responding
            //
            // Get all files last access info
            int result = FileAccess.GetFiles(WorkingPath);
            lblFileTotal.Text = "Total number of files: " + result.ToString();

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

                    txtOutput.AppendText(currentFileInfo + Environment.NewLine);
                    Logger.Log(currentFileInfo);

                    StatusTick();

                    treeView1.Nodes.Add("File Name: " + file_Info.FileName);
                    treeView1.Nodes[^1].Nodes.Add("File Path: " + file_Info.FilePath);
                    treeView1.Nodes[^1].Nodes.Add("File Size: " + file_Info.FileSize);
                    treeView1.Nodes[^1].Nodes.Add("Creation Time: " + file_Info.CreationTime);
                    treeView1.Nodes[^1].Nodes.Add("Last Write Time: " + file_Info.LastWriteTime);
                    treeView1.Nodes[^1].Nodes.Add("Last Access Time: " + file_Info.LastAccessTime);
                }

                btnExport.Enabled = true;
                btnRun.Enabled = false;
                lblStatus.Text = "Status: idle";
            }
            else
            {
                txtOutput.AppendText("No files found in the Working Path." + Environment.NewLine);
                Logger.Log("No files found in the Working Path.");
                lblStatus.Text = "Status: idle";
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            FrmViewer viewer = new();
            viewer.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string CSVFilePath = FUZDirectory + @"\files.csv";

            try
            {
                FileAccess.ExportDataTableToCSV(FileAccess.filesDT, CSVFilePath);
                btnView.Enabled = true;
                btnExport.Enabled = false;
                MessageBox.Show("The data has been exported to " + CSVFilePath, "File Utility Zero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Log($"An error occurred while exporting the data to CSV: {ex.Message}");
            }
        }

        private void BtnView_Click_1(object sender, EventArgs e)
        {
            FrmViewer viewer = new();
            viewer.ShowDialog();
        }
    }
}
