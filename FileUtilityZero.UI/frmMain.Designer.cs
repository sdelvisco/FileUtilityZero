

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
            progressBar = new ProgressBar();
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
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle defaultRowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            //
            // btnBrowse
            //
            btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBrowse.BackColor = Color.White;
            btnBrowse.Cursor = Cursors.Hand;
            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.ForeColor = Color.FromArgb(32, 32, 32);
            btnBrowse.Location = new Point(772, 400);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += BtnBrowse_Click;
            //
            // btnRun
            //
            btnRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRun.BackColor = Color.FromArgb(0, 120, 215);
            btnRun.Cursor = Cursors.Hand;
            btnRun.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btnRun.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 160);
            btnRun.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 110, 190);
            btnRun.FlatStyle = FlatStyle.Flat;
            btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRun.ForeColor = Color.White;
            btnRun.Location = new Point(610, 429);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 1;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += BtnRun_Click;
            //
            // btnExit
            //
            btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExit.BackColor = Color.White;
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.FromArgb(32, 32, 32);
            btnExit.Location = new Point(772, 429);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            //
            // txtWorkingPath
            //
            txtWorkingPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtWorkingPath.BorderStyle = BorderStyle.FixedSingle;
            txtWorkingPath.Location = new Point(311, 400);
            txtWorkingPath.Name = "txtWorkingPath";
            txtWorkingPath.Size = new Size(455, 23);
            txtWorkingPath.TabIndex = 3;
            //
            // label1
            //
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(223, 404);
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
            lblStatus.Location = new Point(18, 404);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(64, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: idle";
            //
            // btnExport
            //
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExport.BackColor = Color.White;
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.ForeColor = Color.FromArgb(32, 32, 32);
            btnExport.Location = new Point(691, 429);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 9;
            btnExport.Text = "Export csv";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += BtnExport_Click;
            //
            // lblFileCount
            //
            lblFileCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileCount.AutoSize = true;
            lblFileCount.ForeColor = Color.FromArgb(90, 90, 90);
            lblFileCount.Location = new Point(18, 353);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(148, 15);
            lblFileCount.TabIndex = 11;
            lblFileCount.Text = "Number of files scanned: 0";
            //
            // lblFileTotal
            //
            lblFileTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileTotal.AutoSize = true;
            lblFileTotal.ForeColor = Color.FromArgb(90, 90, 90);
            lblFileTotal.Location = new Point(311, 353);
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
            chkIncludeHash.Location = new Point(18, 458);
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
            chkIncludeCategory.Location = new Point(250, 458);
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
            txtFilter.BorderStyle = BorderStyle.FixedSingle;
            txtFilter.Location = new Point(85, 12);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(756, 23);
            txtFilter.TabIndex = 17;
            txtFilter.TextChanged += TxtFilter_TextChanged;
            //
            // progressBar
            //
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(18, 374);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(823, 14);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 19;
            progressBar.Visible = false;
            //
            // columnHeaderStyle
            //
            columnHeaderStyle.BackColor = Color.FromArgb(245, 245, 245);
            columnHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            columnHeaderStyle.ForeColor = Color.FromArgb(40, 40, 40);
            columnHeaderStyle.Padding = new Padding(6, 0, 6, 0);
            columnHeaderStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            columnHeaderStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            //
            // defaultRowStyle
            //
            defaultRowStyle.BackColor = Color.White;
            defaultRowStyle.ForeColor = Color.FromArgb(32, 32, 32);
            defaultRowStyle.Padding = new Padding(6, 2, 6, 2);
            defaultRowStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            defaultRowStyle.SelectionForeColor = Color.White;
            //
            // alternatingRowStyle
            //
            alternatingRowStyle.BackColor = Color.FromArgb(248, 248, 248);
            //
            // dgvResults
            //
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AlternatingRowsDefaultCellStyle = alternatingRowStyle;
            dgvResults.AutoGenerateColumns = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.FixedSingle;
            dgvResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvResults.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dgvResults.ColumnHeadersHeight = 32;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] { colFileName, colFilePath, colFileSize, colCreationTime, colLastWriteTime, colLastAccessTime, colExtension, colAttributes, colIsReadOnly, colDirectoryName, colFileHash, colCategory });
            dgvResults.DefaultCellStyle = defaultRowStyle;
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.GridColor = Color.FromArgb(230, 230, 230);
            dgvResults.Location = new Point(18, 41);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.RowTemplate.Height = 24;
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
            BackColor = Color.White;
            ClientSize = new Size(859, 495);
            Controls.Add(dgvResults);
            Controls.Add(progressBar);
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
            MinimumSize = new Size(700, 420);
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
        private ProgressBar progressBar;
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
