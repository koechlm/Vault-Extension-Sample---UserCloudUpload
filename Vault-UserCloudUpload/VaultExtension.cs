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

[assembly: ApiVersion("15.0")]
[assembly: ExtensionId("6b8d6d58-b035-49ce-95d7-abdebb0404e1")]

namespace VaultUserCloudUpload
{
    public class VaultExtension : IExplorerExtension
    {
        public static VDF.Vault.Currency.Connections.Connection mConnection = null;
        public static Settings mSettings = null;
        public static bool mConfigPerm = false;
        public static bool mSettingsChanged = false;
        public List<ACW.File> mFilesToUpload = new List<ACW.File>();
        public List<long> mFileIds = new List<long>();

        //public bool mIsDarkTheme = VDF.Forms.SkinUtils.ThemeState.IsDarkTheme;


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

        private void mUploadToCloudProjectCmd_Execute(object sender, CommandItemEventArgs e)
        {
            //get the selected files from Context.CurrentSelectionSet
            foreach (ISelection entity in e.Context.CurrentSelectionSet)
            {
                mFileIds.Add(entity.Id);
            }

            //get the dialog to prepare its content
            UploadPreview mUploadPreview = new UploadPreview(mFileIds);

            DialogResult dialogResult = mUploadPreview.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

            }
        }

        private void mUploadToCloudCmd_Execute(object sender, CommandItemEventArgs e)
        {
            SelectProjects mSelectProjects = new SelectProjects();
            DialogResult dialogResult = mSelectProjects.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                mUploadToCloudProjectCmd_Execute(sender, e);
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
                //if (mIsDarkTheme)
                //{
                //    //Autodesk.iLogic.ThemeSkins.CustomThemeSkins.Register();
                //    mAdminWindow.LookAndFeel.SetSkinStyle(Autodesk.iLogic.ThemeSkins.CustomThemeSkins.DarkThemeName);
                //    mAdminWindow.IconOptions.Image = Properties.Resources.AdminOptionsImage_16_dark;
                //}
                //else
                //{
                //    mAdminWindow.LookAndFeel.SetSkinStyle(Autodesk.iLogic.ThemeSkins.CustomThemeSkins.LightThemeName);
                //mAdminWindow.IconOptions.Image = Properties.Resources.AdminOptionsImage_16_light;
                //}
                //if (Autodesk.DataManagement.Client.Framework.Forms.SkinUtils.WinFormsTheme.Instance.CurrentTheme == VDF.Forms.SkinUtils.Theme.Default)
                //{
                //    mAdminWindow.LookAndFeel.SetOffice2003Style();
                //}

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
            return null;
        }

        public IEnumerable<string> HiddenCommands()
        {
            return null;
        }

        public void OnLogOff(IApplication application)
        {
            //do nothing
        }

        public void OnLogOn(IApplication application)
        {
            mConnection = application.Connection;
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
