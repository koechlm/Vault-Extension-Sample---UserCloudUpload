/*=====================================================================
  
  This file is part of the Autodesk Vault API Code Samples.

  Copyright (C) Autodesk Inc.  All rights reserved.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
=====================================================================*/
using System.IO;
using System.Xml.Serialization;

using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;

namespace VaultUserCloudUpload
{
    [XmlRoot("settings")]
    public class Settings
    {
        private static string SettingsVaultOptionsName = "Autodesk.Vault.UserCloudUpload.Settings";

        [XmlElement("FileNameSuffixes")]
        public string FileNameSuffixies;

        [XmlElement("VaultFolderCat")]
        public string VaultFolderCat;

        [XmlElement("CloudDrivePath")]
        public string CloudDrivePath;

        [XmlElement("CloudPath")]
        public string CloudPath;

        [XmlElement("DriveTypes")]
        public string[] DriveTypes;

        [XmlElement("EnabledFileFormats")]
        public string[] EnabledFileFormats;


        public Settings()
        {

        }

        public static Settings LoadFromVault(Connection connection)
        {
            Settings retval = null;

            string settingsString = connection.WebServiceManager.KnowledgeVaultService.GetVaultOption(
                SettingsVaultOptionsName);
            if (settingsString != null && settingsString.Length > 0)
            {
                try
                {
                    using (System.IO.StringReader reader = new System.IO.StringReader(settingsString))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                        retval = (Settings)serializer.Deserialize(reader);
                    }
                }
                catch
                { }
            }
            return retval;
        }

        public bool SaveToVault(Connection connection)
        {
            string settingsString = null;

            try
            {
                using (System.IO.StringWriter writer = new System.IO.StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(writer, this);
                    settingsString = writer.ToString();
                }
                connection.WebServiceManager.KnowledgeVaultService.SetVaultOption(SettingsVaultOptionsName, settingsString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetSettingsPath()
        {
            string codeFolder = Util.GetAssemblyPath();
            string xmlPath = Path.Combine(codeFolder, "Vault.UserCloudUploadSettings.xml");
            return xmlPath;
        }

        public bool Save()
        {
            try
            {                
                string xmlPath = GetSettingsPath();

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(xmlPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(writer, this);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Settings Load()
        {
            Settings retVal = new Settings();

            string xmlPath = GetSettingsPath();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(xmlPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                retVal = (Settings)serializer.Deserialize(reader);
            }


            return retVal;
        }
    }

}
