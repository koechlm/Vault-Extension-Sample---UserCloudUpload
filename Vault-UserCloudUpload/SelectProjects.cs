using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VaultUserCloudUpload
{
    public partial class SelectProjects : Form
    {
        public SelectProjects()
        {
            InitializeComponent();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                btnContinue.Enabled = true;
            }
            else
            {
                btnContinue.Enabled = false;
            }
        }

        private void btnContinue_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
