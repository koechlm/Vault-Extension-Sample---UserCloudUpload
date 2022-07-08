using System;
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
    public partial class UploadPreview : Form
    {
        public static Settings mSettings = null;
        private Vault.Currency.Connections.Connection mConnection = VaultUserCloudUpload.VaultExtension.mConnection;

        public UploadPreview(List<long> mFileIds, ref VDF.Vault.Settings.AcquireFilesSettings mAcquireSettings)
        {
            InitializeComponent();

            //toDo - replace by LoadFromVault
            mSettings = Settings.Load();
            string mNameSuffix1 = mSettings.FileNameSuffixies.Split(',').FirstOrDefault().TrimEnd();
            string mNameSuffix2 = mSettings.FileNameSuffixies.Split(',').Last().TrimStart();

            //Get the property definitions for FILE and FLDR
            ACW.PropDef[] mFilePropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            ACW.PropDef[] mFolderPropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FLDR");

            long mSuffixId1 = mFilePropDefs.Where(n => n.DispName == mNameSuffix1).FirstOrDefault().Id;
            long mSuffixId2 = mFilePropDefs.Where(n => n.DispName == mNameSuffix2).FirstOrDefault().Id;

            ACW.File[] mFiles = mConnection.WebServiceManager.DocumentService.GetLatestFilesByMasterIds(mFileIds.ToArray());
            List<long> mFileIterationIds = new List<long>();
            foreach (ACW.File file in mFiles)
            {
                mFileIterationIds.Add(file.Id);
            }
            ACW.PropInst[] mAllFilesPropInsts = mConnection.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FILE", mFileIterationIds.ToArray());

            List<ACW.File> mFilesToUpload = new List<ACW.File>();
            mFilesToUpload = mFiles.ToList<ACW.File>();

            foreach (ACW.File file in mFilesToUpload)
            {
                //preset error handling for each file
                bool mValidPath = false;
                bool mValidFileName = true;

                //get the property instances of the individual file
                IEnumerable<ACW.PropInst> mFilePropInsts = mAllFilesPropInsts.Where(n => n.EntityId == file.Id);

                //build the file name; the original name might get two configured suffixes to indicate content and revision
                //toDo - handle property value types if needed
                string mSuffix1 = null;
                string mSuffix2 = null;
                string mRestrictionText = null;
                var temp1 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId1).FirstOrDefault().Val;
                if (temp1 is null)
                {
                    mSuffix1 = String.Format("[{0}]", mNameSuffix1);
                    mValidFileName = false;
                    mRestrictionText = "Warning - One of the file name suffixes misses a value; check the property marked with '[]'.";
                }
                else
                {
                    mSuffix1 = temp1.ToString();
                }
                var temp2 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId2).FirstOrDefault().Val;
                if (temp2 is null)
                {
                    mSuffix2 = String.Format("[{0}]", mNameSuffix2);
                    mValidFileName = false;
                    mRestrictionText = "Warning - One of the file name suffixes misses a value; check the property marked with '[]'.";
                }
                else
                {
                    mSuffix2 = temp2.ToString();
                }
                string mFileExt = '.' + file.VerName.Split('.').Last();
                string mFileName = file.VerName.Replace(mFileExt, "");
                string mUploadFileName = mFileName + "--" + mSuffix1 + "--" + mSuffix2 + mFileExt;

                //get and validate the parent projects cloud project path mapping
                ACW.Folder mFolder = mConnection.WebServiceManager.DocumentService.GetFolderById(file.FolderId);
                ACW.Folder mProjectFldr = null;
                string mCldDrvPath = null;

                if (mFolder.Cat.CatName == mSettings.VaultFolderCat)
                {
                    mProjectFldr = mFolder;
                }
                else
                {
                    if (mFolder.FullName != "$/")
                    {
                        do
                        {
                            mFolder = mConnection.WebServiceManager.DocumentService.GetFolderById(mFolder.ParId);
                            if (mFolder.Cat.CatName == mSettings.VaultFolderCat)
                            {
                                mProjectFldr = mFolder;
                            }
                        } while (mProjectFldr != null && mFolder.FullName == "$");
                    }
                }

                if (mProjectFldr != null)
                {
                    ACW.PropInst[] mFldrPropInsts = mConnection.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FLDR", new long[] { mProjectFldr.Id });
                    long mCldDrvPropId = mFolderPropDefs.Where(n => n.DispName == mSettings.CloudDrivePath).FirstOrDefault().Id;

                    mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\" + mFldrPropInsts.Where(n => n.PropDefId == mCldDrvPropId).FirstOrDefault().Val.ToString();
                    mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                    btnUpload.Enabled = true;

                    //try to find a corresponding mapped drive project name
                    if (mValidPath != true)
                    {
                        List<string> mEnabledDrives = mSettings.DriveTypes.Split(',').ToList();
                        foreach (string item in mEnabledDrives)
                        {
                            item.TrimEnd().TrimStart();
                        }

                        //try to find an corresponding (Vault project name == cloud project name) download folder on ADocs or Fusion Team
                        if (mEnabledDrives.Contains("Drive"))
                        {
                            mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs\\" + mProjectFldr.Name + "\\Project Files";
                            mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                        }
                        if (mValidPath != true && mEnabledDrives.Contains("Fusion"))
                        {
                            mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion\\" + mProjectFldr.Name;
                            mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                        }
                        //finally
                        if (mValidPath != true)
                        {
                            mCldDrvPath = "";
                            mRestrictionText = "Could not find corresponding or registered cloud project";
                        }
                    }
                    else
                    {
                        //add the individual file and its target download path to the AcquisitionOptions
                        VDF.Vault.Currency.Entities.FileIteration mFileIt = new VDF.Vault.Currency.Entities.FileIteration(mConnection, file);
                        VDF.Currency.FilePathAbsolute mLocalPath = new VDF.Currency.FilePathAbsolute(mCldDrvPath + "\\" + mUploadFileName);
                        mAcquireSettings.AddFileToAcquire(mFileIt, mAcquireSettings.DefaultAcquisitionOption, mLocalPath);
                    }

                }
                else //neither configured nor corresponding project found; file will not download - set an error on this row below
                {
                    mCldDrvPath = "";
                    mRestrictionText = "Could not find corresponding or registered cloud project";
                }

                //add the files to the preview list and mark an error if the target path or filename is not validated successfully
                if (mValidPath == false)
                {
                    dtGrdUploadFiles.Rows.Add(false, mUploadFileName, mCldDrvPath, mRestrictionText);
                    dtGrdUploadFiles.Rows[dtGrdUploadFiles.RowCount - 1].ErrorText = "File will not upload";
                }
                else
                {
                    dtGrdUploadFiles.Rows.Add(true, mUploadFileName, mCldDrvPath, mRestrictionText);
                }
            }

        }


        private void btnUpload_MouseClick(object sender, MouseEventArgs e)
        {
            //perform the download of all files valid for download

            this.DialogResult = DialogResult.OK;
            dtGrdUploadFiles.Rows.Clear();
            dtGrdUploadFiles.Refresh();
            this.Close();
        }

        private void btnUploadCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
