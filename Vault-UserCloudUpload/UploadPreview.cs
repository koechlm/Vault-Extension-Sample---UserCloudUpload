using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public UploadPreview(List<long> mFileIds, ref Dictionary<string, VDF.Vault.Settings.AcquireFilesSettings> mAcquireDict, List<string> mUserSelectedProjects = null)
        {
            InitializeComponent();

            mSettings = Settings.LoadFromVault(mConnection);

            VaultExtension.mEnabledDrives = mSettings.DriveTypes.ToList();
            foreach (string item in VaultExtension.mEnabledDrives)
            {
                item.Replace(" ", "");
            }
            string mAllowedDrives = "Autodesk ";
            foreach (var item in VaultExtension.mEnabledDrives)
            {
                mAllowedDrives = mAllowedDrives + "| " + item;
            }

            string mNameSuffix1 = mSettings.FileNameSuffixies.Split(',').FirstOrDefault().TrimEnd();
            string mNameSuffix2 = mSettings.FileNameSuffixies.Split(',').Last().TrimStart();

            //Get the property definitions for FILE and FLDR
            ACW.PropDef[] mFilePropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            ACW.PropDef[] mFolderPropDefs = VaultUserCloudUpload.VaultExtension.mFldrPropDefs;

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

            foreach (ACW.File mFile in mFilesToUpload)
            {
                //a file must not add multiple times to one acquire package (settings); we build individual acquiresettings for each file
                string mKey = mFile.MasterId.ToString();
                VDF.Vault.Settings.AcquireFilesSettings mAcquireSettings = null;

                //preset error handling for each file
                bool mValidPath = false;
                bool mValidFileExt = false;

                //get the property instances of the individual file
                IEnumerable<ACW.PropInst> mFilePropInsts = mAllFilesPropInsts.Where(n => n.EntityId == mFile.Id);

                //build the file name; the original name might get two configured suffixes to indicate content and revision
                //toDo - handle property value types if needed
                string mFileExt = '.' + mFile.VerName.Split('.').Last();
                string mFileName = mFile.VerName.Replace(mFileExt, "");
                string mSuffix1 = null;
                string mSuffix2 = null;
                string mRestrictionText = null;
                ACW.PropInst mPrpInst1 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId1).FirstOrDefault();
                if (mPrpInst1 is null)
                {
                    mSuffix1 = String.Format("[{0}]", mNameSuffix1);
                    mRestrictionText = "Warning - One of the file name suffixes misses a value; check the property marked with '[]'.";
                }
                else
                {
                    mSuffix1 = mPrpInst1.Val.ToString();
                    //remove characters not allowed for filenames
                    mSuffix1 = String.Format("{0}", Regex.Replace(mSuffix1, @"[\/?:*""><|]+", "", RegexOptions.Compiled));
                }
                mFileName = mFileName + "-" + mSuffix1;

                ACW.PropInst mPrpInst2 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId2).FirstOrDefault();
                if (mPrpInst2 is null)
                {
                    mSuffix2 = String.Format("[{0}]", mNameSuffix2);
                    mRestrictionText = "Warning - One of the file name suffixes misses a value; check the property marked with '[]'.";
                }
                else
                {
                    mSuffix2 = mPrpInst2.Val.ToString();
                    //remove characters not allowed for filenames
                    mSuffix2 = String.Format("{0}", Regex.Replace(mSuffix2, @"[\/?:*""><|]+", "", RegexOptions.Compiled));
                }
                mFileName = mFileName + "-" + mSuffix2;

                string mUploadFileName = mFileName + mFileExt;

                //variables required to validate the parent projects cloud project path mapping
                ACW.Folder mTempFldr = null;
                string mSubFldr = null;
                ACW.Folder mFolder = mConnection.WebServiceManager.DocumentService.GetFolderById(mFile.FolderId);
                ACW.Folder mMappedFldr = null;

                //differentiate user selected projects vs. configured/corresponding projects
                string mCldDrvPath = null;
                if (mUserSelectedProjects != null)
                {
                    //add the individual file to each validated project path
                    foreach (string mPath in mUserSelectedProjects)
                    {
                        mValidPath = (new System.IO.DirectoryInfo(mPath)).Exists;
                        if (mValidPath != true)
                        {
                            mCldDrvPath = "";
                            mRestrictionText = "Could not validate the selected project.";
                        }
                        else //valid local path
                        {
                            mCldDrvPath = mPath;

                            if ((mPath + "\\" + mUploadFileName).ToString().Length > 256)
                            {
                                mRestrictionText = "The resulting file name and path exceeds 256 characters.";
                            }
                            else //file path length < 256
                            {
                                // check that the current file's extension is listed for the cloud drive found
                                mValidFileExt = mCheckValidFileExt(mCldDrvPath, mFileExt);
                                if (mValidFileExt == false)
                                {
                                    mRestrictionText = "File Extension " + mFileExt + " is not enabled for the target drive";
                                    mValidFileExt = false;
                                }
                                else
                                {
                                    //add the individual file and its target download path to the AcquisitionOptions
                                    VDF.Vault.Currency.Entities.FileIteration mFileIt = new VDF.Vault.Currency.Entities.FileIteration(mConnection, mFile);
                                    VDF.Currency.FilePathAbsolute mLocalPath = new VDF.Currency.FilePathAbsolute(mCldDrvPath + "\\" + mUploadFileName);

                                    //the current file shares to 1 to n projects; this requires individual acquire packages for each target
                                    mAcquireSettings = VaultExtension.CreateAcquireSettings();
                                    mKey = mFile.MasterId.ToString() + mLocalPath.ToString();
                                    mAcquireDict.Add(mKey, mAcquireSettings);
                                    mAcquireDict[mKey].AddFileToAcquire(mFileIt, mAcquireDict[mKey].DefaultAcquisitionOption, mLocalPath);
                                }
                            }

                            //add the files to the preview list and mark an error if the target path or file is not validated successfully
                            if (string.IsNullOrEmpty(mSubFldr) != true)
                            {
                                mCldDrvPath = mCldDrvPath + mSubFldr;
                            }
                            if (mValidPath == false || mValidFileExt == false)
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
                }
                else //configured or corresponding(=matching names) projects
                {
                    if (mFolder.Cat.CatName == mSettings.VaultFolderCat)
                    {
                        mMappedFldr = mFolder;
                    }
                    else
                    {
                        if (mFolder.FullName != "$/")
                        {
                            do
                            {
                                mTempFldr = mConnection.WebServiceManager.DocumentService.GetFolderById(mFolder.ParId);
                                if (mTempFldr.Cat.CatName == mSettings.VaultFolderCat)
                                {
                                    mMappedFldr = mTempFldr;
                                }
                            } while (mMappedFldr != null && mTempFldr.FullName == "$");
                        }
                    }

                    if (mMappedFldr != null)
                    {
                        ACW.PropInst[] mFldrPropInsts = mConnection.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FLDR", new long[] { mMappedFldr.Id });
                        long mCldDrvPropId = mFolderPropDefs.Where(n => n.DispName == mSettings.CloudDrivePath).FirstOrDefault().Id;

                        mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\" + mFldrPropInsts.Where(n => n.PropDefId == mCldDrvPropId).FirstOrDefault().Val.ToString();
                        mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;

                        //grab the subfolder tree from project to the file's parent
                        mSubFldr = mFolder.FullName.Replace(mMappedFldr.FullName, "").Replace("/", "\\");

                        //try to find a corresponding mapped drive project name
                        if (mValidPath != true)
                        {
                            //try to find an corresponding (Vault project name == cloud project name) download folder on ADocs or Fusion Team
                            if (VaultExtension.mEnabledDrives.Contains("Drive"))
                            {
                                mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ADrive\\" + mMappedFldr.Name + "\\Project Files";
                                mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                                if (string.IsNullOrEmpty(mSubFldr) != true)
                                {
                                    mCldDrvPath = mCldDrvPath + mSubFldr;
                                }
                            }
                            if (VaultExtension.mEnabledDrives.Contains("Docs"))
                            {
                                mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs\\" + mMappedFldr.Name + "\\Project Files";
                                mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                                if (string.IsNullOrEmpty(mSubFldr) != true)
                                {
                                    mCldDrvPath = mCldDrvPath + mSubFldr;
                                }
                            }
                            if (mValidPath != true && VaultExtension.mEnabledDrives.Contains("Fusion"))
                            {
                                mCldDrvPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion\\" + mMappedFldr.Name;
                                mValidPath = (new System.IO.DirectoryInfo(mCldDrvPath)).Exists;
                                if (string.IsNullOrEmpty(mSubFldr) != true)
                                {
                                    mCldDrvPath = mCldDrvPath + mSubFldr;
                                }
                            }
                            //finally
                            if (mValidPath != true)
                            {
                                mCldDrvPath = "";
                                mRestrictionText = "Could not find corresponding or registered cloud project";
                            }
                        }
                        else //mValidPath == true
                        {
                            //check the selection against configured/allowed cloud drives
                            string mSelectedDrive = null;
                            if (mCldDrvPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ADrive"))
                            {
                                mSelectedDrive = "Drive";
                            }
                            if (mCldDrvPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs"))
                            {
                                mSelectedDrive = "Docs";
                            }
                            if (mCldDrvPath.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion"))
                            {
                                mSelectedDrive = "Fusion";
                            }

                            if (mSelectedDrive != null && VaultExtension.mEnabledDrives.Contains(mSelectedDrive))
                            {
                                if ((mCldDrvPath + "\\" + mUploadFileName).ToString().Length > 256)
                                {
                                    mRestrictionText = "The resulting file name and path exceeds 256 characters.";
                                }
                                else //file path length < 256
                                {
                                    // check that the current file's extension is listed for the cloud drive found
                                    mValidFileExt = mCheckValidFileExt(mCldDrvPath, mFileExt);
                                    if (mValidFileExt == false)
                                    {
                                        mRestrictionText = "File Extension " + mFileExt + " is not enabled for the target drive";
                                    }
                                    else
                                    {
                                        //add the individual file and its target download path to the AcquisitionOptions
                                        mAcquireSettings = VaultExtension.CreateAcquireSettings();
                                        mAcquireDict.Add(mKey, mAcquireSettings);
                                        if (string.IsNullOrEmpty(mSubFldr) != true)
                                        {
                                            mCldDrvPath = mCldDrvPath + mSubFldr;
                                        }
                                        VDF.Vault.Currency.Entities.FileIteration mFileIt = new VDF.Vault.Currency.Entities.FileIteration(mConnection, mFile);
                                        VDF.Currency.FilePathAbsolute mLocalPath = new VDF.Currency.FilePathAbsolute(mCldDrvPath + "\\" + mUploadFileName);
                                        mAcquireDict[mKey].AddFileToAcquire(mFileIt, mAcquireDict[mKey].DefaultAcquisitionOption, mLocalPath);
                                    }
                                } //file path length < 256
                            }
                            else
                            {
                                mValidPath = false;
                                mRestrictionText = "Selection does not match with an allowed cloud drive of " + mAllowedDrives;
                            }

                        } //mValidPath == true
                    }
                    else //neither configured nor corresponding project found; file will not download - set an error on this row below
                    {
                        mCldDrvPath = "";
                        //mValidPath default value = false
                        mRestrictionText = "Could not find corresponding or registered cloud project";
                    }

                    //add the files to the preview list and mark an error if the target path or file is not validated successfully
                    if (string.IsNullOrEmpty(mSubFldr) != true)
                    {
                        mCldDrvPath = mCldDrvPath + mSubFldr';
                    }
                    if (mValidPath == false || mValidFileExt == false)
                    {
                        dtGrdUploadFiles.Rows.Add(false, mUploadFileName, mCldDrvPath, mRestrictionText);
                        dtGrdUploadFiles.Rows[dtGrdUploadFiles.RowCount - 1].ErrorText = "File will not upload";
                    }
                    else
                    {
                        dtGrdUploadFiles.Rows.Add(true, mUploadFileName, mCldDrvPath, mRestrictionText);
                    }

                } //configured or corresponding projects

            } //mFile in mFilesToUpload

            if (mAcquireDict.Count > 0)
            {
                btnUpload.Enabled = true;
            }

        } // end of UploadPreview


        private bool mCheckValidFileExt(string path, string ext)
        {

            if (VaultExtension.mEnabledExtns == null)
            {
                VaultExtension.mEnabledExtns = VaultExtension.mGetDriveFileTypes();
            }

            string mSelectedDrive = null;
            if (path.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ADrive"))
            {
                mSelectedDrive = "Autodesk Drive";
            }
            if (path.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\ACCDocs"))
            {
                mSelectedDrive = "Autodesk Docs";
            }
            if (path.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Fusion"))
            {
                mSelectedDrive = "Autodesk Fusion Team";
            }

            if (VaultExtension.mEnabledExtns[mSelectedDrive].Contains(ext.ToUpper()))
            {
                return true;
            }
            return false;
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

        private void dtGrdUploadFiles_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dtGrdUploadFiles.Rows.Count == 0)
            {
                btnUpload.Enabled = false;
            }
        }

        //private void dtGrdUploadFiles_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        //{
        //    btnUpload.Enabled = true;
        //}
    }
}
