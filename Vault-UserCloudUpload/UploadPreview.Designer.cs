
namespace VaultUserCloudUpload
{
    partial class UploadPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadPreview));
            this.dtGrdUploadFiles = new System.Windows.Forms.DataGridView();
            this.Upload = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UploadFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnUploadCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdUploadFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGrdUploadFiles
            // 
            this.dtGrdUploadFiles.AllowUserToAddRows = false;
            this.dtGrdUploadFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGrdUploadFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdUploadFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Upload,
            this.UploadFilename,
            this.ProjectPath});
            this.dtGrdUploadFiles.Location = new System.Drawing.Point(0, 0);
            this.dtGrdUploadFiles.Name = "dtGrdUploadFiles";
            this.dtGrdUploadFiles.ReadOnly = true;
            this.dtGrdUploadFiles.Size = new System.Drawing.Size(702, 359);
            this.dtGrdUploadFiles.TabIndex = 0;
            // 
            // Upload
            // 
            this.Upload.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Upload.FalseValue = "False";
            this.Upload.FillWeight = 10F;
            this.Upload.HeaderText = "Validated";
            this.Upload.Name = "Upload";
            this.Upload.ReadOnly = true;
            this.Upload.ToolTipText = "Only validated files will upload";
            this.Upload.TrueValue = "True";
            // 
            // UploadFilename
            // 
            this.UploadFilename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UploadFilename.FillWeight = 40F;
            this.UploadFilename.HeaderText = "Upload Filename";
            this.UploadFilename.Name = "UploadFilename";
            this.UploadFilename.ReadOnly = true;
            this.UploadFilename.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadFilename.ToolTipText = "The file naming convention is an administrative option";
            // 
            // ProjectPath
            // 
            this.ProjectPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProjectPath.FillWeight = 50F;
            this.ProjectPath.HeaderText = "Project Path";
            this.ProjectPath.Name = "ProjectPath";
            this.ProjectPath.ReadOnly = true;
            this.ProjectPath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProjectPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProjectPath.ToolTipText = "Corresponding path of mapped cloud drive.";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.AutoSize = true;
            this.btnUpload.Image = global::VaultUserCloudUpload.Properties.Resources.cmdPush_16_light;
            this.btnUpload.Location = new System.Drawing.Point(597, 365);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Padding = new System.Windows.Forms.Padding(5);
            this.btnUpload.Size = new System.Drawing.Size(93, 33);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUploadCancel
            // 
            this.btnUploadCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUploadCancel.AutoSize = true;
            this.btnUploadCancel.Location = new System.Drawing.Point(531, 365);
            this.btnUploadCancel.Name = "btnUploadCancel";
            this.btnUploadCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnUploadCancel.Size = new System.Drawing.Size(60, 33);
            this.btnUploadCancel.TabIndex = 2;
            this.btnUploadCancel.Text = "Cancel";
            this.btnUploadCancel.UseVisualStyleBackColor = true;
            // 
            // UploadPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 410);
            this.Controls.Add(this.btnUploadCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dtGrdUploadFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UploadPreview";
            this.Text = "Upload to Cloud - Preview";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdUploadFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGrdUploadFiles;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnUploadCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Upload;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectPath;
    }
}