using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VDF = Autodesk.DataManagement.Client.Framework;

namespace VaultUserCloudUpload
{
    public partial class AdminOptions : Form
    {
        public AdminOptions()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                Settings mNewSettings = Settings.Load();

                //Mapping tab
                cmbSuffix1UDP.Text = mNewSettings.FileNameSuffixies.Split(',').FirstOrDefault();
                cmbSuffix2UDP.Text = mNewSettings.FileNameSuffixies.Split(',').LastOrDefault();

                for (int i = 0; i < mNewSettings.DriveTypes.Count(); i++)
                {
                    if (mNewSettings.DriveTypes[i] == "Drive")
                    {
                        chckdListDriveTypes.SetItemCheckState(i, CheckState.Checked);
                    }
                    if (mNewSettings.DriveTypes[i] == "Docs")
                    {
                        chckdListDriveTypes.SetItemCheckState(i, CheckState.Checked);
                    }
                    if (mNewSettings.DriveTypes[i] == "Fusion")
                    {
                        chckdListDriveTypes.SetItemCheckState(i, CheckState.Checked);
                    }
                }

                cmbFldCategory.Text = mNewSettings.VaultFolderCat;
                cmbCloudDrivePathUDP.Text = mNewSettings.CloudDrivePath;
                cmbCloudDriveUrlUDP.Text = mNewSettings.CloudPath;

                //Conversion Tab
                mUpdateDrvConfSettingsGrid(mNewSettings.DriveConversionSettings);

            }
            catch (Exception)
            {
                VDF.Forms.Library.ShowError("Could not import settings. Check the permissions Vault Extensions folder.", "Configuration Import");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Settings mActiveSettings = new Settings();
            //Mapping tab
            mActiveSettings.FileNameSuffixies = cmbSuffix1UDP.Text + "," + cmbSuffix2UDP.Text;
            mActiveSettings.DriveTypes = mGetDriveTypes();
            mActiveSettings.VaultFolderCat = cmbFldCategory.Text;
            mActiveSettings.CloudDrivePath = cmbCloudDrivePathUDP.Text;
            mActiveSettings.CloudPath = cmbCloudDriveUrlUDP.Text;
            //Conversion tab
            mActiveSettings.DriveConversionSettings = mGetDrvConvSettings();

            bool mExpSuccess = mActiveSettings.Save();
            if (mExpSuccess == true)
            {
                VDF.Forms.Library.ShowMessage("Successfully exported settings to local file.", "Configuration Export", VDF.Forms.Currency.ButtonConfiguration.Ok);
            }
            else
            {
                VDF.Forms.Library.ShowError("Export settings to local file failed. Check the permissions Vault Extensions folder.", "Configuration Export");
            }

        }

        private string[] mGetDriveTypes()
        {
            string[] mDrvTypes = new string[3];

            if (chckdListDriveTypes.GetItemChecked(0))
            {
                mDrvTypes[0] = "Drive";
            }
            if (chckdListDriveTypes.GetItemChecked(1))
            {
                mDrvTypes[1] = "Docs";
            }
            if (chckdListDriveTypes.GetItemChecked(2))
            {
                mDrvTypes[2] = "Fusion";
            }
            return mDrvTypes;
        }

        private string[] mGetDrvConvSettings()
        {
            string[] mDrvConfSettings = new string[dtGrdConvSettings.RowCount];

            for (int i = 0; i < dtGrdConvSettings.Rows.Count; i++)
            {
                if (dtGrdConvSettings.Rows[i].Cells[0].Value != null)
                {
                    mDrvConfSettings[i] = dtGrdConvSettings.Rows[i].Cells[0].Value.ToString() + "|"
                        + dtGrdConvSettings.Rows[i].Cells[1].Value.ToString() + "|"
                        + dtGrdConvSettings.Rows[i].Cells[2].Value.ToString() + "|"
                        + dtGrdConvSettings.Rows[i].Cells[3].EditedFormattedValue;
                }
            }
            return mDrvConfSettings;
        }

        private void mUpdateDrvConfSettingsGrid(string[] mDrvConfSettings)
        {
            //clear existing Data
            dtGrdConvSettings.Rows.Clear();
            //write settings
            if (mDrvConfSettings != null)
            {
                string[] mRow = null;
                for (int i = 0; i < mDrvConfSettings.Count(); i++)
                {
                    mRow = mDrvConfSettings[i].Split('|');
                    dtGrdConvSettings.Rows.Add();
                    dtGrdConvSettings.Rows[i].Cells[0].Value = mRow[0];
                    dtGrdConvSettings.Rows[i].Cells[1].Value = mRow[1];
                    dtGrdConvSettings.Rows[i].Cells[2].Value = mRow[2];
                    dtGrdConvSettings.Rows[i].Cells[3].Value = mRow[3];
                }
            }
        }

    }
}
