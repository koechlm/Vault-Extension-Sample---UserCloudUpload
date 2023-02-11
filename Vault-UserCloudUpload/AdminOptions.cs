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
        private bool mVaultSettingsLoaded = false;

        public AdminOptions()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (mVaultSettingsLoaded)
            {
                var retval = VDF.Forms.Library.ShowMessage("Importing settings will overwrite all current configuration settings.\n\r\n\rPress OK to load, press Cancel to keep current settings.", "Load Settings", VDF.Forms.Currency.ButtonConfiguration.OkCancel);
                if (retval == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                Settings mNewSettings = Settings.Load();

                //Mapping tab
                cmbSuffix1UDP.Text = mNewSettings.FileNameSuffixies.Split(',').FirstOrDefault();
                cmbSuffix2UDP.Text = mNewSettings.FileNameSuffixies.Split(',').LastOrDefault();

                foreach (var item in mNewSettings.DriveTypes)
                {
                    if (item == "Drive")
                    {
                        chckdListDriveTypes.SetItemCheckState(0, CheckState.Checked);
                    }
                    if (item == "Docs")
                    {
                        chckdListDriveTypes.SetItemCheckState(1, CheckState.Checked);
                    }
                    if (item == "Fusion")
                    {
                        chckdListDriveTypes.SetItemCheckState(2, CheckState.Checked);
                    }
                }
                cmbFldCategory.Text = mNewSettings.VaultFolderCat;
                cmbCloudDrivePathUDP.Text = mNewSettings.CloudDrivePath;
                cmbCloudDriveUrlUDP.Text = mNewSettings.CloudPath;

                //Conversion Tab
                mUpdateDrvConfSettingsGrid(mNewSettings.DriveConversionSettings);

                mVaultSettingsLoaded = true;
            }
            catch (Exception)
            {
                VDF.Forms.Library.ShowError("An error occurred while loading settings from Vault.\n\r\n\rCheck folder read permissions. Or the setting file's content.", "Configuration Import");
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

        private void btnLoadFromVault_Click(object sender, EventArgs e)
        {
            if (mVaultSettingsLoaded)
            {
                var retval = VDF.Forms.Library.ShowMessage("Loading settings from Vault will overwrite all current configuration settings.\n\r\n\rPress OK to load, press Cancel to keep current settings.", "Load Settings", VDF.Forms.Currency.ButtonConfiguration.OkCancel);
                if (retval == DialogResult.Cancel)
                {
                    return;
                }
            }

            mLoadSettingsFromVault();
        }

        private void mLoadSettingsFromVault()
        {
            try
            {
                Settings mNewSettings = Settings.LoadFromVault(VaultExtension.mConnection);

                //Mapping tab
                cmbSuffix1UDP.Text = mNewSettings.FileNameSuffixies.Split(',').FirstOrDefault();
                cmbSuffix2UDP.Text = mNewSettings.FileNameSuffixies.Split(',').LastOrDefault();

                foreach (var item in mNewSettings.DriveTypes)
                {
                    if (item == "Drive")
                    {
                        chckdListDriveTypes.SetItemCheckState(0, CheckState.Checked);
                    }
                    if (item == "Docs")
                    {
                        chckdListDriveTypes.SetItemCheckState(1, CheckState.Checked);
                    }
                    if (item == "Fusion")
                    {
                        chckdListDriveTypes.SetItemCheckState(2, CheckState.Checked);
                    }
                }

                cmbFldCategory.Text = mNewSettings.VaultFolderCat;
                cmbCloudDrivePathUDP.Text = mNewSettings.CloudDrivePath;
                cmbCloudDriveUrlUDP.Text = mNewSettings.CloudPath;

                //Conversion Tab
                mUpdateDrvConfSettingsGrid(mNewSettings.DriveConversionSettings);

                mVaultSettingsLoaded = true;
            }
            catch (Exception)
            {
                VDF.Forms.Library.ShowError("An error occurred while loading settings from Vault.", "Load Configuration");
            }
        }

        private void btnSaveToVault_Click(object sender, EventArgs e)
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

            bool mExpSuccess = mActiveSettings.SaveToVault(VaultExtension.mConnection);
            if (mExpSuccess == true)
            {
                VDF.Forms.Library.ShowMessage("Successfully exported settings to local file.", "Save Configuration", VDF.Forms.Currency.ButtonConfiguration.Ok);
            }
            else
            {
                VDF.Forms.Library.ShowError("Export settings to local file failed. Check the permissions Vault Extensions folder.", "Save Configuration");
            }
        }

        private void AdminOptions_Load(object sender, EventArgs e)
        {
            VaultExtension.mFilePropDispNames = VaultExtension.mGetFilePropNames();
            cmbSuffix1UDP.Items.AddRange(VaultExtension.mFilePropDispNames.ToArray());
            cmbSuffix2UDP.Items.AddRange(VaultExtension.mFilePropDispNames.ToArray());
            cmbFldCategory.Items.AddRange(VaultExtension.mGetFldCatNames().ToArray());
            VaultExtension.mFldrPropNames = VaultExtension.mGetFldPropNames();
            cmbCloudDrivePathUDP.Items.AddRange(VaultExtension.mFldrPropNames.ToArray());
            cmbCloudDriveUrlUDP.Items.AddRange(VaultExtension.mFldrPropNames.ToArray());
            mLoadSettingsFromVault();
        }
    }
}
