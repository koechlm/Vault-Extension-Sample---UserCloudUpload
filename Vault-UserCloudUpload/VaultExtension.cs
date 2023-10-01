using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ACW = Autodesk.Connectivity.WebServices;
using ACWT = Autodesk.Connectivity.WebServicesTools;
using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.Extensibility.Framework;

[assembly: ApiVersion("16.0")]
[assembly: ExtensionId("6b8d6d58-b035-49ce-95d7-abdebb0404e1")]

namespace VaultUserCloudUpload
{
    public class VaultExtension : IExplorerExtension
    {
        public static VDF.Vault.Currency.Connections.Connection mConnection = null;
        public static ACWT.WebServiceManager mWsMgr = null;
        public static Settings mSettings = null;
        public static bool mConfigPerm = false;
        public static bool mSettingsChanged = false;
        public static ACW.PropDef[] mFldrPropDefs = null;
        public static List<string> mFldrPropNames = new List<string>();
        public static List<string> mFilePropDispNames = new List<string>();
        public static List<string> mFldrCatNames = new List<string>();
        public static List<string> mEnabledDrives = null;
        public static Dictionary<string, List<string>> mEnabledExtns = null;
        private string mCurrentTheme;


        IEnumerable<CommandSite> IExplorerExtension.CommandSites()
        {
            List<CommandSite> mVaultExtensionCmdSites = new List<CommandSite>();

            //Describe admin command item
            CommandItem mAdminOptionsCmd = new CommandItem("Command.VaultUserUploadAdminForm", "Configure User Cloud Upload...");
            //if (mIsDarkTheme)
            //{
            //    mAdminOptionsCmd.Image = Properties.Resources.AdminOptionsImage_16_dark;
            //}
            //else
            //{
            mAdminOptionsCmd.Image = Properties.Resources.AdminOptionsImage_16_light;
            //}
            mAdminOptionsCmd.Execute += mAdminCmd_Execute;

            //Deploy command site
            CommandSite mAdminCmdSite = new CommandSite("Menu.ToolsMenu", "VaultUserUploadAdminOptions")
            {
                Location = CommandSiteLocation.ToolsMenu,
                DeployAsPulldownMenu = false
            };
            mAdminCmdSite.AddCommand(mAdminOptionsCmd);
            mVaultExtensionCmdSites.Add(mAdminCmdSite);

            //describe user context menu commands
            CommandItem mUploadToCloudProjectCmd = new CommandItem("Command.UploadToCloudProject", "Upload to Cloud Project");
            mUploadToCloudProjectCmd.NavigationTypes = new SelectionTypeId[] { SelectionTypeId.File };
            mUploadToCloudProjectCmd.MultiSelectEnabled = true;
            mUploadToCloudProjectCmd.Hint = "Uploads selected file(s) to the corresponding or configured cloud project";
            mUploadToCloudProjectCmd.Execute += mUploadToCloudProjectCmd_Execute;
            //if (mIsDarkTheme)
            //{
            //    mUploadToCloudProjectCmd.Image = Properties.Resources.cmdPush_16_dark;
            //}
            //else
            //{
            mUploadToCloudProjectCmd.Image = Properties.Resources.cmdPush_16_light;
            //}

            CommandItem mUploadToCloudCmd = new CommandItem("Command.UploadToCloud", "Upload to ...");
            mUploadToCloudCmd.NavigationTypes = new SelectionTypeId[] { SelectionTypeId.File };
            mUploadToCloudCmd.MultiSelectEnabled = true;
            mUploadToCloudCmd.Hint = "Select Cloud Drive and upload selected file(s)";
            mUploadToCloudCmd.Execute += mUploadToCloudCmd_Execute;
            //if (mIsDarkTheme)
            //{
            //    mUploadToCloudCmd.Image = Properties.Resources.GlobalFolderView_16_dark;
            //}
            //else
            //{
            mUploadToCloudCmd.Image = Properties.Resources.GlobalFolderView_16_light;
            //}

            //deploy command site for the file context commands
            CommandSite mUploadToCloudCmdSite = new CommandSite("Menu.FileContextMenu", "Upload to Cloud");
            mUploadToCloudCmdSite.Location = CommandSiteLocation.FileContextMenu;
            mUploadToCloudCmdSite.DeployAsPulldownMenu = false;
            mUploadToCloudCmdSite.AddCommand(mUploadToCloudProjectCmd);
            mUploadToCloudCmdSite.AddCommand(mUploadToCloudCmd);

            mVaultExtensionCmdSites.Add(mUploadToCloudCmdSite);

            return mVaultExtensionCmdSites;
        }

        public static List<string> mGetFilePropNames()
        {
            mWsMgr = mConnection.WebServiceManager;
            ACW.PropDef[] mPropDefs = mWsMgr.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");
            List<string> mPropNames = new List<string>();
            foreach (var item in mPropDefs)
            {
                mPropNames.Add(item.DispName);
            }
            return mPropNames;
        }

        public static List<string> mGetFldPropNames()
        {
            mWsMgr = mConnection.WebServiceManager;
            ACW.PropDef[] mPropDefs = mWsMgr.PropertyService.GetPropertyDefinitionsByEntityClassId("FLDR");
            List<string> mPropNames = new List<string>();
            foreach (var item in mPropDefs)
            {
                mPropNames.Add(item.DispName);
            }
            return mPropNames;
        }

        public static List<string> mGetFldCatNames()
        {
            mWsMgr = mConnection.WebServiceManager;
            ACW.Cat[] mCats = mWsMgr.CategoryService.GetCategoriesByEntityClassId("FLDR", true);
            List<string> mCatNames = new List<string>();
            foreach (var item in mCats)
            {
                mCatNames.Add(item.Name);
            }
            return mCatNames;
        }

        public static Dictionary<string, List<string>> mGetDriveFileTypes()
        {
            Dictionary<string, List<string>> mDriveFileTypes = new Dictionary<string, List<string>>();

            mSettings = Settings.LoadFromVault(mConnection);
            string mDrive = null;
            string mExtns = null;
            List<string> mExtensions = new List<string>();

            foreach (var item in mSettings.EnabledFileFormats)
            {
                mDrive = item.Split('|')[0];
                mExtns = item.Split('|')[1].Replace(" ", "").Replace("*", "");

                if (mDriveFileTypes.TryGetValue(mDrive, out mExtensions))
                {   //existing key|value pair -> add the new values
                    mExtensions = mExtns.Split(',').ToList();
                    mDriveFileTypes[mDrive].AddRange(mExtensions);
                }
                else
                {   //new key|value pair -> add new key and value
                    mExtensions = mExtns.Split(',').ToList();
                    mDriveFileTypes.Add(mDrive, mExtensions);
                }
            }

            return mDriveFileTypes;
        }

        private void mUploadToCloudProjectCmd_Execute(object sender, CommandItemEventArgs e)
        {
            //id collection of files to process
            List<long> mFileIds = new List<long>();

            //define download settings as a default for all files
            VDF.Vault.Settings.AcquireFilesSettings mAcquireSettings = CreateAcquireSettings();
            Dictionary<string, VDF.Vault.Settings.AcquireFilesSettings> mAcquireDict = new Dictionary<string, VDF.Vault.Settings.AcquireFilesSettings>();

            //get the file ids from the user's selection
            foreach (ISelection entity in e.Context.CurrentSelectionSet)
            {
                mFileIds.Add(entity.Id);
            }

            //get the dialog to prepare its content
            UploadPreview mUploadPreview = new UploadPreview(mFileIds, ref mAcquireDict);

            DialogResult dialogResult = mUploadPreview.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                //download files iterating the AcquireList
                foreach (var downloadpackage in mAcquireDict)
                {
                    VDF.Vault.Results.AcquireFilesResults results = mConnection.FileManager.AcquireFiles(downloadpackage.Value);
                }

            }

            mUploadPreview.Dispose();

        }


        public static VDF.Vault.Settings.AcquireFilesSettings CreateAcquireSettings()
        {
            VDF.Vault.Settings.AcquireFilesSettings settings = new VDF.Vault.Settings.AcquireFilesSettings(mConnection);
            settings.DefaultAcquisitionOption = VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Checkout;
            settings.DefaultAcquisitionOption = VDF.Vault.Settings.AcquireFilesSettings.AcquisitionOption.Download;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeChildren = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeParents = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.RecurseParents = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.RecurseChildren = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeAttachments = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.IncludeLibraryContents = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.ReleaseBiased = true;
            settings.OptionsRelationshipGathering.FileRelationshipSettings.VersionGatheringOption = VDF.Vault.Currency.VersionGatheringOption.Revision;
            settings.OptionsRelationshipGathering.IncludeLinksSettings.IncludeLinks = false;
            VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions mResOpt = new VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions();
            mResOpt.OverwriteOption = VDF.Vault.Settings.AcquireFilesSettings.AcquireFileResolutionOptions.OverwriteOptions.ForceOverwriteAll;
            mResOpt.SyncWithRemoteSiteSetting = VDF.Vault.Settings.AcquireFilesSettings.SyncWithRemoteSite.Always;

            return settings;
        }

        private void mUploadToCloudCmd_Execute(object sender, CommandItemEventArgs e)
        {
            //id collection of files to process
            List<long> mFileIds = new List<long>();

            //define download settings as a default for all files
            VDF.Vault.Settings.AcquireFilesSettings mAcquireSettings = CreateAcquireSettings();
            Dictionary<string, VDF.Vault.Settings.AcquireFilesSettings> mAcquireDict = new Dictionary<string, VDF.Vault.Settings.AcquireFilesSettings>();

            //get the file ids from the user's selection
            foreach (ISelection entity in e.Context.CurrentSelectionSet)
            {
                mFileIds.Add(entity.Id);
            }

            //get the dialog to select target projects for the upload
            List<String> mUserSelectedProjects = new List<string>();
            SelectProjects mSelectProjects = new SelectProjects();
            DialogResult mSelectProjectsResult = mSelectProjects.ShowDialog();

            if (mSelectProjectsResult == DialogResult.OK)
            {
                mUserSelectedProjects = mSelectProjects.mProjects;
                //get the dialog to prepare its content
                UploadPreview mUploadPreview = new UploadPreview(mFileIds, ref mAcquireDict, mUserSelectedProjects);

                DialogResult dialogResult = mUploadPreview.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    //download files iterating the AcquireList
                    foreach (var downloadpackage in mAcquireDict)
                    {
                        VDF.Vault.Results.AcquireFilesResults results = mConnection.FileManager.AcquireFiles(downloadpackage.Value);
                    }
                }

                mUploadPreview.Dispose();
            }
        }

        private void mAdminCmd_Execute(object sender, CommandItemEventArgs e)
        {
            Autodesk.Connectivity.WebServices.Permis[] mAllPermisObjects = e.Context.Application.Connection.WebServiceManager.AdminService.GetPermissionsByUserId(e.Context.Application.Connection.UserID);
            List<long> mAllPermissions = new List<long>();
            foreach (var item in mAllPermisObjects)
            {
                mAllPermissions.Add(item.Id);
            }
            if (mAllPermissions.Contains(76) && mAllPermissions.Contains(77)) //76 = Vault Set Options; 77 = Vault Get Options
            {
                mConfigPerm = true;
                AdminOptions mAdminWindow = new AdminOptions();

                mCurrentTheme = VDF.Forms.SkinUtils.WinFormsTheme.Instance.CurrentTheme.ToString();
                if (mCurrentTheme == VDF.Forms.SkinUtils.Theme.Light.ToString())
                {
                    mAdminWindow.LookAndFeel.SetSkinStyle(VDF.Forms.SkinUtils.CustomThemeSkins.LightThemeName);
                    mAdminWindow.IconOptions.Image = Properties.Resources.AdminOptionsImage_16_dark;
                }

                if (mCurrentTheme == VDF.Forms.SkinUtils.Theme.Dark.ToString())
                {
                    mAdminWindow.LookAndFeel.SetSkinStyle(VDF.Forms.SkinUtils.CustomThemeSkins.DarkThemeName);
                    mAdminWindow.IconOptions.Image = Properties.Resources.AdminOptionsImage_16_light;
                }
                if (mCurrentTheme == VDF.Forms.SkinUtils.Theme.Default.ToString())
                {
                    mAdminWindow.LookAndFeel.SetSkinStyle(VDF.Forms.SkinUtils.CustomThemeSkins.DefaultThemeName);
                    mAdminWindow.IconOptions.Image = Properties.Resources.AdminOptionsImage_16_light;
                }

                mAdminWindow.ShowDialog();
            }
            else
            {
                //VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Vault User-Cloud-Upload Administration Options", "Access denied", "Accessing Administration Options requires the permissions 'Vault Get Options' and 'Vault Set Options'");
                VDF.Currency.Restriction mAdminRestriction = new VDF.Currency.Restriction("Administration Options", "Access is limited to Configuration Administrators");
                VDF.Forms.Settings.ShowRestrictionsSettings showRestrictionsSettings = new VDF.Forms.Settings.ShowRestrictionsSettings("Vault User-Cloud-Upload", VDF.Forms.Settings.ShowRestrictionsSettings.IconType.Error);
                showRestrictionsSettings.AddRestriction(mAdminRestriction);
                showRestrictionsSettings.ShowDetailsArea = true;
                showRestrictionsSettings.RestrictionColumnCaption = "Restriction";
                showRestrictionsSettings.RestrictedObjectNameColumnCaption = "Object";
                //showRestrictionsSettings.ReasonColumnCaption = "Restriction Reason";

                DialogResult result = VDF.Forms.Library.ShowRestrictions(showRestrictionsSettings);
            }
        }



        public IEnumerable<CustomEntityHandler> CustomEntityHandlers()
        {
            return null;
        }

        public IEnumerable<DetailPaneTab> DetailTabs()
        {
            // Create a DetailPaneTab list to return from method
            List<DetailPaneTab> mTabs = new List<DetailPaneTab>();

            // Create Selection Info tab for Files
            DetailPaneTab mBrowserTab = new DetailPaneTab("Fldr.Tab.browsertab",
                                                        "Cloud Project View",
                                                        SelectionTypeId.Folder,
                                                        typeof(BrowserControl)); //type of our UserControl

            //The propertyTab_SelectionChanged is called whenever our tab is active and the selection    
            //changes in the main grid 
            mBrowserTab.SelectionChanged += mBrowserTab_SelectionChanged;
            mTabs.Add(mBrowserTab);

            return mTabs;

        }

        private void mBrowserTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Context.SelectedObject != null && e.Context.SelectedObject.TypeId.EntityClassId == "FLDR")
            {
                try
                {
                    long FolderId = e.Context.SelectedObject.Id;
                    //check if the current folder is of configured mapped category; iterate parents if not
                    ACW.Folder mFolder = mConnection.WebServiceManager.DocumentService.GetFolderById(FolderId);
                    if (!(mFolder.Cat.CatName == mSettings.VaultFolderCat))
                    {
                        if (mFolder.FullName != "$")
                        {
                            do
                            {
                                mFolder = mConnection.WebServiceManager.DocumentService.GetFolderById(mFolder.ParId);
                                if (mFolder.Cat.CatName == mSettings.VaultFolderCat)
                                {
                                    FolderId = mFolder.Id;
                                    break;
                                }
                            } while (mFolder.FullName != "$");
                        }
                    }

                    //get the selected folder's property values
                    ACW.PropInst[] mSourcePropInsts = mConnection.WebServiceManager.PropertyService.GetPropertiesByEntityIds("FLDR", new long[] { FolderId });
                    string mPropDispName = mSettings.CloudPath;
                    long mPropId = mFldrPropDefs.Where(n => n.DispName == mSettings.CloudPath).FirstOrDefault().Id;
                    string mPropVal = null;
                    //it might happen that the prop is not assigned to a folder
                    try
                    {
                        mPropVal = (string)mSourcePropInsts.Where(n => n.PropDefId == mPropId).FirstOrDefault().Val;
                    }
                    catch (Exception)
                    {
                        mPropVal = "about:blank";
                    }

                    if (mPropVal == null || mPropVal == "")
                    {
                        mPropVal = "about:blank";
                    }

                    // The event args has our custom tab object.  We need to cast it to our type.
                    BrowserControl tabControl = e.Context.UserControl as BrowserControl;

                    // activate the URL in the tab's web viewer
                    tabControl.mNavigate(mPropVal);
                }
                catch (Exception ex)
                {
                    // If something goes wrong, we don't want the exception to bubble up to Vault Explorer.
                    VDF.Forms.Library.ShowError("Error: " + ex.Message, "Vault2Cloud User Upload Sample - View Tab");
                }
            }


        }

        public IEnumerable<string> HiddenCommands()
        {
            //toDo hide the user interactive project sync commands
            return null;
        }

        public void OnLogOff(IApplication application)
        {
            //do nothing
        }

        public void OnLogOn(IApplication application)
        {
            mConnection = application.Connection;
            mSettings = Settings.LoadFromVault(mConnection);
            mFldrPropDefs = mConnection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FLDR");

            //mIsDarkTheme = VDF.Forms.SkinUtils.ThemeState.IsDarkTheme;
        }

        public void OnShutdown(IApplication application)
        {
            //do nothing
        }

        public void OnStartup(IApplication application)
        {
            VDF.Library.Initialize();
            VDF.Forms.Library.Initialize();
        }

    }
}
