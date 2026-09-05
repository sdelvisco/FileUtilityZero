

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
            chkIncludeHash = new CheckBox();
            chkIncludeCategory = new CheckBox();
            lblFilter = new Label();
            txtFilter = new TextBox();
            dgvResults = new DataGridView();
            colFileName = new DataGridViewTextBoxColumn();
            colFilePath = new DataGridViewTextBoxColumn();
            colFileSize = new DataGridViewTextBoxColumn();
            colCreationTime = new DataGridViewTextBoxColumn();
            colLastWriteTime = new DataGridViewTextBoxColumn();
            colLastAccessTime = new DataGridViewTextBoxColumn();
            colExtension = new DataGridViewTextBoxColumn();
            colAttributes = new DataGridViewTextBoxColumn();
            colIsReadOnly = new DataGridViewTextBoxColumn();
            colDirectoryName = new DataGridViewTextBoxColumn();
            colFileHash = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            //
            // btnBrowse
            //
            btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBrowse.Location = new Point(772, 386);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += BtnBrowse_Click;
            //
            // btnRun
            //
            btnRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRun.Location = new Point(610, 415);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 1;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += BtnRun_Click;
            //
            // btnExit
            //
            btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExit.Location = new Point(772, 415);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExit_Click;
            //
            // txtWorkingPath
            //
            txtWorkingPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtWorkingPath.Location = new Point(311, 386);
            txtWorkingPath.Name = "txtWorkingPath";
            txtWorkingPath.Size = new Size(455, 23);
            txtWorkingPath.TabIndex = 3;
            //
            // label1
            //
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(223, 390);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Working Path:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(18, 390);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(64, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: idle";
            //
            // btnExport
            //
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExport.Location = new Point(691, 415);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 9;
            btnExport.Text = "Export csv";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            //
            // lblFileCount
            //
            lblFileCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileCount.AutoSize = true;
            lblFileCount.Location = new Point(18, 359);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(148, 15);
            lblFileCount.TabIndex = 11;
            lblFileCount.Text = "Number of files scanned: 0";
            //
            // lblFileTotal
            //
            lblFileTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileTotal.AutoSize = true;
            lblFileTotal.Location = new Point(311, 359);
            lblFileTotal.Name = "lblFileTotal";
            lblFileTotal.Size = new Size(167, 15);
            lblFileTotal.TabIndex = 12;
            lblFileTotal.Text = "Total number of files in path: 0";
            //
            // chkIncludeHash
            //
            chkIncludeHash.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkIncludeHash.AutoSize = true;
            chkIncludeHash.Checked = false;
            chkIncludeHash.Location = new Point(18, 444);
            chkIncludeHash.Name = "chkIncludeHash";
            chkIncludeHash.Size = new Size(180, 19);
            chkIncludeHash.TabIndex = 14;
            chkIncludeHash.Text = "Include file hash (SHA-256)";
            chkIncludeHash.UseVisualStyleBackColor = true;
            //
            // chkIncludeCategory
            //
            chkIncludeCategory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkIncludeCategory.AutoSize = true;
            chkIncludeCategory.Checked = false;
            chkIncludeCategory.Location = new Point(250, 444);
            chkIncludeCategory.Name = "chkIncludeCategory";
            chkIncludeCategory.Size = new Size(140, 19);
            chkIncludeCategory.TabIndex = 15;
            chkIncludeCategory.Text = "Include file category";
            chkIncludeCategory.UseVisualStyleBackColor = true;
            //
            // lblFilter
            //
            lblFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblFilter.AutoSize = true;
            lblFilter.Location = new Point(18, 15);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(46, 15);
            lblFilter.TabIndex = 16;
            lblFilter.Text = "Search:";
            //
            // txtFilter
            //
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.Location = new Point(85, 12);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(756, 23);
            txtFilter.TabIndex = 17;
            txtFilter.TextChanged += TxtFilter_TextChanged;
            //
            // dgvResults
            //
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AutoGenerateColumns = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] { colFileName, colFilePath, colFileSize, colCreationTime, colLastWriteTime, colLastAccessTime, colExtension, colAttributes, colIsReadOnly, colDirectoryName, colFileHash, colCategory });
            dgvResults.Location = new Point(18, 41);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(823, 300);
            dgvResults.TabIndex = 18;
            dgvResults.ColumnHeaderMouseClick += DgvResults_ColumnHeaderMouseClick;
            //
            // colFileName
            //
            colFileName.DataPropertyName = "FileName";
            colFileName.HeaderText = "File Name";
            colFileName.Name = "colFileName";
            colFileName.ReadOnly = true;
            colFileName.SortMode = DataGridViewColumnSortMode.Programmatic;
            colFileName.Width = 150;
            //
            // colFilePath
            //
            colFilePath.DataPropertyName = "FilePath";
            colFilePath.HeaderText = "File Path";
            colFilePath.Name = "colFilePath";
            colFilePath.ReadOnly = true;
            colFilePath.SortMode = DataGridViewColumnSortMode.Programmatic;
            colFilePath.Width = 220;
            //
            // colFileSize
            //
            colFileSize.DataPropertyName = "FileSize";
            colFileSize.DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            colFileSize.HeaderText = "File Size";
            colFileSize.Name = "colFileSize";
            colFileSize.ReadOnly = true;
            colFileSize.SortMode = DataGridViewColumnSortMode.Programmatic;
            colFileSize.Width = 90;
            //
            // colCreationTime
            //
            colCreationTime.DataPropertyName = "CreationTime";
            colCreationTime.HeaderText = "Creation Time";
            colCreationTime.Name = "colCreationTime";
            colCreationTime.ReadOnly = true;
            colCreationTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            colCreationTime.Width = 130;
            //
            // colLastWriteTime
            //
            colLastWriteTime.DataPropertyName = "LastWriteTime";
            colLastWriteTime.HeaderText = "Last Write Time";
            colLastWriteTime.Name = "colLastWriteTime";
            colLastWriteTime.ReadOnly = true;
            colLastWriteTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            colLastWriteTime.Width = 130;
            //
            // colLastAccessTime
            //
            colLastAccessTime.DataPropertyName = "LastAccessTime";
            colLastAccessTime.HeaderText = "Last Access Time";
            colLastAccessTime.Name = "colLastAccessTime";
            colLastAccessTime.ReadOnly = true;
            colLastAccessTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            colLastAccessTime.Width = 130;
            //
            // colExtension
            //
            colExtension.DataPropertyName = "Extension";
            colExtension.HeaderText = "Extension";
            colExtension.Name = "colExtension";
            colExtension.ReadOnly = true;
            colExtension.SortMode = DataGridViewColumnSortMode.Programmatic;
            colExtension.Width = 70;
            //
            // colAttributes
            //
            colAttributes.DataPropertyName = "Attributes";
            colAttributes.HeaderText = "Attributes";
            colAttributes.Name = "colAttributes";
            colAttributes.ReadOnly = true;
            colAttributes.SortMode = DataGridViewColumnSortMode.Programmatic;
            colAttributes.Width = 140;
            //
            // colIsReadOnly
            //
            colIsReadOnly.DataPropertyName = "IsReadOnly";
            colIsReadOnly.HeaderText = "Is Read Only";
            colIsReadOnly.Name = "colIsReadOnly";
            colIsReadOnly.ReadOnly = true;
            colIsReadOnly.SortMode = DataGridViewColumnSortMode.Programmatic;
            colIsReadOnly.Width = 80;
            //
            // colDirectoryName
            //
            colDirectoryName.DataPropertyName = "DirectoryName";
            colDirectoryName.HeaderText = "Directory Name";
            colDirectoryName.Name = "colDirectoryName";
            colDirectoryName.ReadOnly = true;
            colDirectoryName.SortMode = DataGridViewColumnSortMode.Programmatic;
            colDirectoryName.Width = 220;
            //
            // colFileHash
            //
            colFileHash.DataPropertyName = "FileHash";
            colFileHash.HeaderText = "File Hash";
            colFileHash.Name = "colFileHash";
            colFileHash.ReadOnly = true;
            colFileHash.SortMode = DataGridViewColumnSortMode.Programmatic;
            colFileHash.Width = 220;
            //
            // colCategory
            //
            colCategory.DataPropertyName = "Category";
            colCategory.HeaderText = "Category";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            colCategory.SortMode = DataGridViewColumnSortMode.Programmatic;
            colCategory.Width = 90;
            //
            // FrmMain
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 480);
            Controls.Add(dgvResults);
            Controls.Add(txtFilter);
            Controls.Add(lblFilter);
            Controls.Add(chkIncludeCategory);
            Controls.Add(chkIncludeHash);
            Controls.Add(lblFileTotal);
            Controls.Add(lblFileCount);
            Controls.Add(btnExport);
            Controls.Add(lblStatus);
            Controls.Add(label1);
            Controls.Add(txtWorkingPath);
            Controls.Add(btnExit);
            Controls.Add(btnRun);
            Controls.Add(btnBrowse);
            MinimumSize = new Size(700, 400);
            Name = "FrmMain";
            Text = "File Utility Zero - Sal Delvisco";
            Load += FrmMain_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
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
        private Label lblStatus;
        private CheckBox chkIncludeHash;
        private CheckBox chkIncludeCategory;
        private Label lblFilter;
        private TextBox txtFilter;
        private DataGridView dgvResults;
        private DataGridViewTextBoxColumn colFileName;
        private DataGridViewTextBoxColumn colFilePath;
        private DataGridViewTextBoxColumn colFileSize;
        private DataGridViewTextBoxColumn colCreationTime;
        private DataGridViewTextBoxColumn colLastWriteTime;
        private DataGridViewTextBoxColumn colLastAccessTime;
        private DataGridViewTextBoxColumn colExtension;
        private DataGridViewTextBoxColumn colAttributes;
        private DataGridViewTextBoxColumn colIsReadOnly;
        private DataGridViewTextBoxColumn colDirectoryName;
        private DataGridViewTextBoxColumn colFileHash;
        private DataGridViewTextBoxColumn colCategory;
    }
}
