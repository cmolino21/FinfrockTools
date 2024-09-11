using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinfrockTools.Settings
{
    public class AddinSettings
    {
        public string BuildVersion = "1.0.1";

        public bool IsWorksetPromptEnabled { get; set; } = true;

        // Add more settings as needed
        // public string SomeOtherSetting { get; set; }

        private static string GetSettingsFilePath()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string addinFolder = Path.Combine(appDataFolder, "FinfrockTools", "RevitAddin");
            Directory.CreateDirectory(addinFolder);
            return Path.Combine(addinFolder, "Settings.xml");
        }

        public static AddinSettings Load()
        {
            string filePath = GetSettingsFilePath();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AddinSettings));
                    return (AddinSettings)serializer.Deserialize(reader);
                }
            }
            return new AddinSettings(); // Return default settings if no file exists
        }

        public void Save()
        {
            string filePath = GetSettingsFilePath();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AddinSettings));
                serializer.Serialize(writer, this);
            }
        }
    }
}
