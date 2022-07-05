
namespace VaultUserCloudUpload
{
    partial class SelectProjects
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Autodesk Drive");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Autodesk Docs");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Autodesk Fusion Team");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectProjects));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(13, 13);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "ADrive";
            treeNode1.Text = "Autodesk Drive";
            treeNode2.Checked = true;
            treeNode2.Name = "ACCDocs";
            treeNode2.Text = "Autodesk Docs";
            treeNode3.Name = "Fusion";
            treeNode3.Text = "Autodesk Fusion Team";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(287, 396);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.Enabled = false;
            this.btnContinue.Location = new System.Drawing.Point(224, 415);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnContinue_MouseClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(143, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SelectProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectProjects";
            this.Text = "SelectProjects";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
    }
}