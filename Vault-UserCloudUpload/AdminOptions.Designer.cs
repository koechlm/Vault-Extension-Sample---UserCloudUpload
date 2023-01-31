
namespace VaultUserCloudUpload
{
    partial class AdminOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminOptions));
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSaveToVault = new System.Windows.Forms.Button();
            this.btnLoadFromVault = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSuffix2UDP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSuffix1UDP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chckdListDriveTypes = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbFldCategory = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCloudDriveUrlUDP = new System.Windows.Forms.ComboBox();
            this.cmbCloudDrivePathUDP = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtGrdConvSettings = new System.Windows.Forms.DataGridView();
            this.TargetDrive = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NativeFormat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TargetFormat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UploadDWF = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdConvSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.AutoSize = true;
            this.btnExport.Location = new System.Drawing.Point(12, 321);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnExport.Size = new System.Drawing.Size(98, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export Settings";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.AutoSize = true;
            this.btnImport.Location = new System.Drawing.Point(12, 292);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnImport.Size = new System.Drawing.Size(98, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import Settings";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSaveToVault
            // 
            this.btnSaveToVault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveToVault.AutoSize = true;
            this.btnSaveToVault.Image = global::VaultUserCloudUpload.Properties.Resources.CheckIn_32_light;
            this.btnSaveToVault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveToVault.Location = new System.Drawing.Point(532, 306);
            this.btnSaveToVault.Name = "btnSaveToVault";
            this.btnSaveToVault.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveToVault.Size = new System.Drawing.Size(123, 38);
            this.btnSaveToVault.TabIndex = 2;
            this.btnSaveToVault.Text = "Save to Vault";
            this.btnSaveToVault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveToVault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveToVault.UseVisualStyleBackColor = true;
            this.btnSaveToVault.Click += new System.EventHandler(this.btnSaveToVault_Click);
            // 
            // btnLoadFromVault
            // 
            this.btnLoadFromVault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFromVault.AutoSize = true;
            this.btnLoadFromVault.Image = global::VaultUserCloudUpload.Properties.Resources.CheckOut_32_light;
            this.btnLoadFromVault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadFromVault.Location = new System.Drawing.Point(393, 306);
            this.btnLoadFromVault.Name = "btnLoadFromVault";
            this.btnLoadFromVault.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnLoadFromVault.Size = new System.Drawing.Size(133, 38);
            this.btnLoadFromVault.TabIndex = 3;
            this.btnLoadFromVault.Text = "Load from Vault";
            this.btnLoadFromVault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadFromVault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadFromVault.UseVisualStyleBackColor = true;
            this.btnLoadFromVault.Click += new System.EventHandler(this.btnLoadFromVault_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbSuffix2UDP);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSuffix1UDP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 55);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Naming Convention";
            // 
            // cmbSuffix2UDP
            // 
            this.cmbSuffix2UDP.FormattingEnabled = true;
            this.cmbSuffix2UDP.Items.AddRange(new object[] {
            "",
            "Revision",
            "Revision Number"});
            this.cmbSuffix2UDP.Location = new System.Drawing.Point(262, 18);
            this.cmbSuffix2UDP.Name = "cmbSuffix2UDP";
            this.cmbSuffix2UDP.Size = new System.Drawing.Size(121, 21);
            this.cmbSuffix2UDP.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 16);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5);
            this.label4.Size = new System.Drawing.Size(26, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = " - ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 16);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(26, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = " - ";
            // 
            // cmbSuffix1UDP
            // 
            this.cmbSuffix1UDP.FormattingEnabled = true;
            this.cmbSuffix1UDP.Location = new System.Drawing.Point(103, 18);
            this.cmbSuffix1UDP.Name = "cmbSuffix1UDP";
            this.cmbSuffix1UDP.Size = new System.Drawing.Size(121, 21);
            this.cmbSuffix1UDP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filename";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chckdListDriveTypes);
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 83);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cloud Drives";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(347, 39);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select all drives exposed for user project selections or the auto-search\r\noption " +
    "for corresponding cloud projects.\r\nProjects with valid Desktop Connector drive m" +
    "appings ignore this option.";
            // 
            // chckdListDriveTypes
            // 
            this.chckdListDriveTypes.FormattingEnabled = true;
            this.chckdListDriveTypes.Items.AddRange(new object[] {
            "Autodesk Drive",
            "Autodesk Docs",
            "Autodesk Fusion Team"});
            this.chckdListDriveTypes.Location = new System.Drawing.Point(9, 20);
            this.chckdListDriveTypes.Name = "chckdListDriveTypes";
            this.chckdListDriveTypes.Size = new System.Drawing.Size(145, 49);
            this.chckdListDriveTypes.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmbFldCategory);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbCloudDriveUrlUDP);
            this.groupBox3.Controls.Add(this.cmbCloudDrivePathUDP);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(0, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(635, 100);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Project Properties";
            // 
            // cmbFldCategory
            // 
            this.cmbFldCategory.FormattingEnabled = true;
            this.cmbFldCategory.Items.AddRange(new object[] {
            "Project"});
            this.cmbFldCategory.Location = new System.Drawing.Point(109, 20);
            this.cmbFldCategory.Name = "cmbFldCategory";
            this.cmbFldCategory.Size = new System.Drawing.Size(144, 21);
            this.cmbFldCategory.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(259, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(313, 26);
            this.label10.TabIndex = 7;
            this.label10.Text = "Folder Category Name of folders to be mapped as corresponding \r\nVault project to " +
    "Cloud proejct.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 20);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5);
            this.label9.Size = new System.Drawing.Size(91, 23);
            this.label9.TabIndex = 6;
            this.label9.Text = "Folder Category";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(259, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(325, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "User Defined Property to store the cloud projects online path (URL).";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(259, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(363, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "User Defined Property to store the mapped local drive (Desktop Connector).";
            // 
            // cmbCloudDriveUrlUDP
            // 
            this.cmbCloudDriveUrlUDP.FormattingEnabled = true;
            this.cmbCloudDriveUrlUDP.Items.AddRange(new object[] {
            "",
            "Cloud Drive URL"});
            this.cmbCloudDriveUrlUDP.Location = new System.Drawing.Point(109, 74);
            this.cmbCloudDriveUrlUDP.Name = "cmbCloudDriveUrlUDP";
            this.cmbCloudDriveUrlUDP.Size = new System.Drawing.Size(144, 21);
            this.cmbCloudDriveUrlUDP.TabIndex = 3;
            // 
            // cmbCloudDrivePathUDP
            // 
            this.cmbCloudDrivePathUDP.FormattingEnabled = true;
            this.cmbCloudDrivePathUDP.Items.AddRange(new object[] {
            "",
            "Cloud Drive Path"});
            this.cmbCloudDrivePathUDP.Location = new System.Drawing.Point(109, 47);
            this.cmbCloudDrivePathUDP.Name = "cmbCloudDrivePathUDP";
            this.cmbCloudDrivePathUDP.Size = new System.Drawing.Size(144, 21);
            this.cmbCloudDrivePathUDP.Sorted = true;
            this.cmbCloudDrivePathUDP.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 72);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5);
            this.label6.Size = new System.Drawing.Size(97, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "Cloud Drive URL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(97, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cloud Drive Path";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 274);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(635, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mapping";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dtGrdConvSettings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(635, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Conversion";
            this.tabPage2.ToolTipText = "Preview - Not implemented yet.";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtGrdConvSettings
            // 
            this.dtGrdConvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdConvSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TargetDrive,
            this.NativeFormat,
            this.TargetFormat,
            this.UploadDWF});
            this.dtGrdConvSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdConvSettings.Location = new System.Drawing.Point(3, 3);
            this.dtGrdConvSettings.Name = "dtGrdConvSettings";
            this.dtGrdConvSettings.Size = new System.Drawing.Size(629, 242);
            this.dtGrdConvSettings.TabIndex = 0;
            // 
            // TargetDrive
            // 
            this.TargetDrive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TargetDrive.FillWeight = 45F;
            this.TargetDrive.HeaderText = "Target Drive";
            this.TargetDrive.Items.AddRange(new object[] {
            "Autodesk Drive",
            "Autodesk Docs",
            "Autodesk Fusion Team"});
            this.TargetDrive.Name = "TargetDrive";
            this.TargetDrive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // NativeFormat
            // 
            this.NativeFormat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NativeFormat.FillWeight = 25F;
            this.NativeFormat.HeaderText = "Native Format";
            this.NativeFormat.Items.AddRange(new object[] {
            "*.IPT",
            "*.IAM",
            "*.IPT, *.IAM",
            "*.IDW"});
            this.NativeFormat.Name = "NativeFormat";
            // 
            // TargetFormat
            // 
            this.TargetFormat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TargetFormat.FillWeight = 20F;
            this.TargetFormat.HeaderText = "Target Format";
            this.TargetFormat.Items.AddRange(new object[] {
            "*.RVT",
            "*.IFC",
            "*.DWG (3D)",
            "*.STP",
            "*.JT",
            "*.PDF",
            "*.DWG (2D)"});
            this.TargetFormat.Name = "TargetFormat";
            // 
            // UploadDWF
            // 
            this.UploadDWF.FalseValue = "false";
            this.UploadDWF.FillWeight = 10F;
            this.UploadDWF.HeaderText = "Upload DWF(x)";
            this.UploadDWF.Name = "UploadDWF";
            this.UploadDWF.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadDWF.ToolTipText = "Create and upload a DWF(x) file if Target Format is empty or in addition to a sel" +
    "ected target format.";
            this.UploadDWF.TrueValue = "true";
            // 
            // AdminOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(667, 356);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnLoadFromVault);
            this.Controls.Add(this.btnSaveToVault);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminOptions";
            this.Text = "AdminOptions";
            this.Load += new System.EventHandler(this.AdminOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdConvSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSaveToVault;
        private System.Windows.Forms.Button btnLoadFromVault;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSuffix2UDP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSuffix1UDP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chckdListDriveTypes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCloudDriveUrlUDP;
        private System.Windows.Forms.ComboBox cmbCloudDrivePathUDP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dtGrdConvSettings;
        private System.Windows.Forms.DataGridViewComboBoxColumn TargetDrive;
        private System.Windows.Forms.DataGridViewComboBoxColumn NativeFormat;
        private System.Windows.Forms.DataGridViewComboBoxColumn TargetFormat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UploadDWF;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbFldCategory;
    }
}