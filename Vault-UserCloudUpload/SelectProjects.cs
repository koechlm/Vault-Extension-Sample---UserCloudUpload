using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACW = Autodesk.Connectivity.WebServices;
using ACWT = Autodesk.Connectivity.WebServicesTools;
using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.Extensibility.Framework;
using Vault = Autodesk.DataManagement.Client.Framework.Vault;

namespace VaultUserCloudUpload
{
    public partial class SelectProjects : Form
    {

        public static Settings mSettings = null;
        private Vault.Currency.Connections.Connection mConnection = VaultUserCloudUpload.VaultExtension.mConnection;
        public List<string> mProjects = new List<string>();

        public SelectProjects()
        {
            InitializeComponent();

            //toDo - replace by LoadFromVault
            mSettings = Settings.Load();
            List<string> mEnabledDrives = mSettings.DriveTypes.Split(',').ToList();
            foreach (string item in mEnabledDrives)
            {
                item.Replace(" ", "");
            }

        }


        private void btnRuleDirAdd_Click(object sender, EventArgs e)
        {
            Image mDriveIcon = null;
            string mProjectName = null;
            folderBrowserDialog1.Description = "Select from Autodesk Docs | Drive | Fusion ...";
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowDialog();

            //Autodesk Drive
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ADrive\\My Data\\"))
            {
                mDriveIcon = imageList1.Images[0];
                mProjectName = "Autodesk Drive / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                mProjects.Add(folderBrowserDialog1.SelectedPath);
            }
            else
            {
                //Restriction Dialog - inform about valid selections Drive: My Data is not a valid selection either!
            }

            //Autodesk Docs
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs\\"))
            {
                mDriveIcon = imageList1.Images[1];
                mProjectName = folderBrowserDialog1.SelectedPath.Split('\\')[4] + " / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                mProjects.Add(folderBrowserDialog1.SelectedPath);
            }
            else
            {
                //Restriction Dialog - inform about valid selections Drive, Autodesk Docs and Fusion
            }

            //Autodesk Fusion 360
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion\\"))
            {
                mDriveIcon = imageList1.Images[2];
                mProjectName = folderBrowserDialog1.SelectedPath.Split('\\')[4] + " / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                mProjects.Add(folderBrowserDialog1.SelectedPath);
            }
            else
            {
                //Restriction Dialog - inform about valid selections Drive, Autodesk Docs and Fusion
            }

            folderBrowserDialog1.Dispose();

            btnContinue.Enabled = true;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            //toDo implement VDF Information Message or Dialog highlighting valid selections of Drive, Autodesk Docs or Fusion 360
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
