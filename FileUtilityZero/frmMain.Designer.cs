

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
            lblStatus = new Label();
            btnExport = new Button();
            lblFileCount = new Label();
            lblFileTotal = new Label();
            txtOutput = new Label();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(772, 147);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += BtnBrowse_Click;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(610, 176);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 1;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += BtnRun_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(772, 176);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExit_Click;
            // 
            // txtWorkingPath
            // 
            txtWorkingPath.Location = new Point(311, 147);
            txtWorkingPath.Name = "txtWorkingPath";
            txtWorkingPath.Size = new Size(455, 23);
            txtWorkingPath.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(223, 151);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Working Path:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(18, 151);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(64, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: idle";
            // 
            // btnExport
            // 
            btnExport.Location = new Point(691, 176);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 9;
            btnExport.Text = "Export csv";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // lblFileCount
            // 
            lblFileCount.AutoSize = true;
            lblFileCount.Location = new Point(18, 120);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(148, 15);
            lblFileCount.TabIndex = 11;
            lblFileCount.Text = "Number of files scanned: 0";
            // 
            // lblFileTotal
            // 
            lblFileTotal.AutoSize = true;
            lblFileTotal.Location = new Point(311, 120);
            lblFileTotal.Name = "lblFileTotal";
            lblFileTotal.Size = new Size(167, 15);
            lblFileTotal.TabIndex = 12;
            lblFileTotal.Text = "Total number of files in path: 0";
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(12, 2);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(835, 118);
            txtOutput.TabIndex = 13;
            txtOutput.Text = "Idle...";
            txtOutput.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 211);
            Controls.Add(txtOutput);
            Controls.Add(lblFileTotal);
            Controls.Add(lblFileCount);
            Controls.Add(btnExport);
            Controls.Add(lblStatus);
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
        private Button btnExport;
        private Label lblFileCount;
        private Label lblFileTotal;
        private Label txtOutput;
        public static Label lblStatus;
    }
}
