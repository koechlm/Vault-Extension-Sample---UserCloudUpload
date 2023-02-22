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
        public List<string> mEnabledDrives = null;
        public List<string> mProjects = new List<string>();

        public SelectProjects()
        {
            InitializeComponent();

            mSettings = Settings.LoadFromVault(mConnection);
            mEnabledDrives = mSettings.DriveTypes.ToList();
            foreach (string item in mEnabledDrives)
            {
                item.Replace(" ", "");
            }

        }


        private void btnRuleDirAdd_Click(object sender, EventArgs e)
        {
            string mAllowedDrives = "Autodesk ";
            foreach (var item in mEnabledDrives)
            {
                mAllowedDrives = mAllowedDrives + "| " + item;
            }
            Image mDriveIcon = null;
            string mProjectName = null;
            folderBrowserDialog1.Description = "Select from " + mAllowedDrives + " ...";
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowDialog();

            //check the selection against configured/allowed cloud drives
            string mSelectedDrive = null;
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ADrive"))
            {
                mSelectedDrive = "Drive";
            }
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs"))
            {
                mSelectedDrive = "Docs";
            }
            if (folderBrowserDialog1.SelectedPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion"))
            {
                mSelectedDrive = "Fusion";
            }

            if (mSelectedDrive != null && mEnabledDrives.Contains(mSelectedDrive))
            {
                //check against individual restrictions per drive
                string[] mSelectedPath = folderBrowserDialog1.SelectedPath.Split('\\');
                //Autodesk Drive
                if (mSelectedDrive == "Drive")
                {
                    if (mSelectedPath.Length > 5) // Element [6] is the first file folder level underneath ADrive\My Data or ADrive\Shared Files
                    {
                        mDriveIcon = imageList1.Images[0];
                        mProjectName = "Autodesk Drive / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                        dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                        mProjects.Add(folderBrowserDialog1.SelectedPath);
                    }
                    else
                    {
                        //Restriction Dialog - the configuration admin did not enable Autodesk Drive
                        //todo for 2023: replace restriction dialog with restriction reason
                        //VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Vault User-Cloud-Upload Administration Options", "Access denied", "Accessing Administration Options requires the permissions 'Vault Get Options' and 'Vault Set Options'");
                        VDF.Currency.Restriction mDrivePathRestriction = new VDF.Currency.Restriction(folderBrowserDialog1.SelectedPath, "Selection does not start within a subfolder in 'My Data' or 'Shared Files'.");
                        VDF.Forms.Settings.ShowRestrictionsSettings mDrivePathRestrictionSettings = new VDF.Forms.Settings.ShowRestrictionsSettings("Vault User-Cloud-Upload", VDF.Forms.Settings.ShowRestrictionsSettings.IconType.Error);
                        mDrivePathRestrictionSettings.AddRestriction(mDrivePathRestriction);
                        mDrivePathRestrictionSettings.SetDescription("Invalid selection.");
                        mDrivePathRestrictionSettings.ShowDetailsArea = true;
                        mDrivePathRestrictionSettings.RestrictionColumnCaption = "Restriction";
                        mDrivePathRestrictionSettings.RestrictedObjectNameColumnCaption = "Selection";
                        //showRestrictionsSettings.ReasonColumnCaption = "Restriction Reason";
                        DialogResult result = VDF.Forms.Library.ShowRestrictions(mDrivePathRestrictionSettings);
                    }
                }

                //Autodesk Docs
                if (mSelectedDrive == "Docs")
                {
                    if (mSelectedPath.Length > 6) // Element [7] is the first file folder level
                    {
                        mDriveIcon = imageList1.Images[1];
                        mProjectName = folderBrowserDialog1.SelectedPath.Split('\\')[4] + " / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                        dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                        mProjects.Add(folderBrowserDialog1.SelectedPath);
                    }
                    else
                    {
                        //Restriction Dialog - the configuration admin did not enable Autodesk Drive
                        //todo for 2023: replace restriction dialog with restriction reason
                        //VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Vault User-Cloud-Upload Administration Options", "Access denied", "Accessing Administration Options requires the permissions 'Vault Get Options' and 'Vault Set Options'");
                        VDF.Currency.Restriction mDocsPathRestriction = new VDF.Currency.Restriction(folderBrowserDialog1.SelectedPath, "Selection does not start within a project's file folder of Autodesk Docs.");
                        VDF.Forms.Settings.ShowRestrictionsSettings mDocsPathRestrictionSettings = new VDF.Forms.Settings.ShowRestrictionsSettings("Vault User-Cloud-Upload", VDF.Forms.Settings.ShowRestrictionsSettings.IconType.Error);
                        mDocsPathRestrictionSettings.AddRestriction(mDocsPathRestriction);
                        mDocsPathRestrictionSettings.SetDescription("Invalid selection.");
                        mDocsPathRestrictionSettings.ShowDetailsArea = true;
                        mDocsPathRestrictionSettings.RestrictionColumnCaption = "Restriction";
                        mDocsPathRestrictionSettings.RestrictedObjectNameColumnCaption = "Selection";
                        //showRestrictionsSettings.ReasonColumnCaption = "Restriction Reason";
                        DialogResult result = VDF.Forms.Library.ShowRestrictions(mDocsPathRestrictionSettings);
                    }
                }

                //Autodesk Fusion 360
                if (mSelectedDrive == "Fusion")
                {
                    if (mSelectedPath.Length > 5) // Element [6] is the first file folder level
                    {
                        mDriveIcon = imageList1.Images[2];
                        mProjectName = folderBrowserDialog1.SelectedPath.Split('\\')[4] + " / " + folderBrowserDialog1.SelectedPath.Split('\\')[5];
                        dataGridView1.Rows.Add(mDriveIcon, mProjectName, folderBrowserDialog1.SelectedPath);
                        mProjects.Add(folderBrowserDialog1.SelectedPath);
                    }
                    else
                    {
                        //Restriction Dialog - the configuration admin did not enable Autodesk Drive
                        //todo for 2023: replace restriction dialog with restriction reason
                        //VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Vault User-Cloud-Upload Administration Options", "Access denied", "Accessing Administration Options requires the permissions 'Vault Get Options' and 'Vault Set Options'");
                        VDF.Currency.Restriction mFusionPathRestriction = new VDF.Currency.Restriction(folderBrowserDialog1.SelectedPath, "Selection does not start within a project of Fusion 360.");
                        VDF.Forms.Settings.ShowRestrictionsSettings mFusionPathRestrictionSettings = new VDF.Forms.Settings.ShowRestrictionsSettings("Vault User-Cloud-Upload", VDF.Forms.Settings.ShowRestrictionsSettings.IconType.Error);
                        mFusionPathRestrictionSettings.AddRestriction(mFusionPathRestriction);
                        mFusionPathRestrictionSettings.SetDescription("Invalid selection.");
                        mFusionPathRestrictionSettings.ShowDetailsArea = true;
                        mFusionPathRestrictionSettings.RestrictionColumnCaption = "Restriction";
                        mFusionPathRestrictionSettings.RestrictedObjectNameColumnCaption = "Selection";
                        //showRestrictionsSettings.ReasonColumnCaption = "Restriction Reason";
                        DialogResult result = VDF.Forms.Library.ShowRestrictions(mFusionPathRestrictionSettings);
                    }

                }
            }
            else //leave and share feedback to the user
            {
                //todo for 2023: replace restriction dialog with restriction reason
                //VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Vault User-Cloud-Upload Administration Options", "Access denied", "Accessing Administration Options requires the permissions 'Vault Get Options' and 'Vault Set Options'");
                VDF.Currency.Restriction mDrivePathRestriction = new VDF.Currency.Restriction(folderBrowserDialog1.SelectedPath, "Selection does not match with an allowed cloud drive of " + mAllowedDrives);
                VDF.Forms.Settings.ShowRestrictionsSettings mDrivePathRestrictionSettings = new VDF.Forms.Settings.ShowRestrictionsSettings("Vault User-Cloud-Upload", VDF.Forms.Settings.ShowRestrictionsSettings.IconType.Error);
                mDrivePathRestrictionSettings.AddRestriction(mDrivePathRestriction);
                mDrivePathRestrictionSettings.SetDescription("Invalid selection.");
                mDrivePathRestrictionSettings.ShowDetailsArea = true;
                mDrivePathRestrictionSettings.RestrictionColumnCaption = "Restriction";
                mDrivePathRestrictionSettings.RestrictedObjectNameColumnCaption = "Selection";
                //showRestrictionsSettings.ReasonColumnCaption = "Restriction Reason";
                DialogResult result = VDF.Forms.Library.ShowRestrictions(mDrivePathRestrictionSettings);

                folderBrowserDialog1.Dispose();
                return;
            }

            folderBrowserDialog1.Dispose();

            if (mProjects.Count >= 1)
            {
                btnContinue.Enabled = true;
            }

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
