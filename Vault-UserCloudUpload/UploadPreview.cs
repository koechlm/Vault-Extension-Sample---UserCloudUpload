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

        public UploadPreview(List<long> mFileIds)
        {
            InitializeComponent();


            //toDo - replace by LoadFromVault
            mSettings = Settings.Load();
            string mNameSuffix1 = mSettings.FileNameSuffixies.Split(',').FirstOrDefault();
            string mNameSuffix2 = mSettings.FileNameSuffixies.Split(',').LastOrDefault().TrimStart();

            //Get the property definitions for FILE and FLDR
            ACW.PropDef[] mFilePropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            ACW.PropDef[] mFolderPropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FLDR");
            bool mValid = false;

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
                IEnumerable<ACW.PropInst> mFilePropInsts = mAllFilesPropInsts.Where(n => n.EntityId == file.Id);
                string mSuffix1 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId1).FirstOrDefault().Val.ToString();
                string mSuffix2 = mFilePropInsts.Where(n => n.PropDefId == mSuffixId2).FirstOrDefault().Val.ToString();
                string mFileExt = '.' + file.VerName.Split('.').Last();
                string mFileName = file.VerName.Replace(mFileExt, "");
                string mUploadFileName = mFileName + "--" + mSuffix1 + "--" + mSuffix2 + mFileExt;

                dtGrdUploadFiles.Rows.Add(mValid, mUploadFileName, file.FolderId);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
