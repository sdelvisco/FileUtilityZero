

namespace FileUtilityZero
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBrowse = new Button();
            btnRun = new Button();
            btnExit = new Button();
            txtWorkingPath = new TextBox();
            label1 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            treeView1 = new TreeView();
            lblStatus = new Label();
            btnExport = new Button();
            btnView = new Button();
            lblFileCount = new Label();
            lblFileTotal = new Label();
            txtOutput = new Label();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(766, 726);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += BtnBrowse_Click;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(1016, 726);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 1;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += BtnRun_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(1097, 726);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExit_Click;
            // 
            // txtWorkingPath
            // 
            txtWorkingPath.Location = new Point(305, 726);
            txtWorkingPath.Name = "txtWorkingPath";
            txtWorkingPath.Size = new Size(455, 23);
            txtWorkingPath.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(217, 730);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Working Path:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(1160, 627);
            treeView1.TabIndex = 6;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 730);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(64, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: idle";
            // 
            // btnExport
            // 
            btnExport.Location = new Point(854, 726);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 9;
            btnExport.Text = "Export csv";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // btnView
            // 
            btnView.Location = new Point(935, 726);
            btnView.Name = "btnView";
            btnView.Size = new Size(75, 23);
            btnView.TabIndex = 10;
            btnView.Text = "View Data";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += BtnView_Click_1;
            // 
            // lblFileCount
            // 
            lblFileCount.AutoSize = true;
            lblFileCount.Location = new Point(12, 699);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(148, 15);
            lblFileCount.TabIndex = 11;
            lblFileCount.Text = "Number of files scanned: 0";
            // 
            // lblFileTotal
            // 
            lblFileTotal.AutoSize = true;
            lblFileTotal.Location = new Point(305, 699);
            lblFileTotal.Name = "lblFileTotal";
            lblFileTotal.Size = new Size(167, 15);
            lblFileTotal.TabIndex = 12;
            lblFileTotal.Text = "Total number of files in path: 0";
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(12, 642);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(1160, 47);
            txtOutput.TabIndex = 13;
            txtOutput.Text = "Idle...";
            txtOutput.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(txtOutput);
            Controls.Add(lblFileTotal);
            Controls.Add(lblFileCount);
            Controls.Add(btnView);
            Controls.Add(btnExport);
            Controls.Add(lblStatus);
            Controls.Add(treeView1);
            Controls.Add(label1);
            Controls.Add(txtWorkingPath);
            Controls.Add(btnExit);
            Controls.Add(btnRun);
            Controls.Add(btnBrowse);
            Name = "FrmMain";
            Text = "File Utility Zero - Sal Delvisco";
            Load += FrmMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Button btnBrowse;
        private Button btnRun;
        private Button btnExit;
        private TextBox txtWorkingPath;
        private Label label1;
        private FolderBrowserDialog folderBrowserDialog1;
        private TreeView treeView1;
        private Button btnExport;
        private Button btnView;
        private Label lblFileCount;
        private Label lblFileTotal;
        private Label txtOutput;
        public static Label lblStatus;
    }
}
