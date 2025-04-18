using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace ExpenseTracker
{
    public class SettingsModel 
    {
        private const string FilePath = "settings.json";


        public bool IsDarkMode { get; set; } = false;
        public string Language { get; set; } = "en";
        public bool NotificationsEnabled { get; set; } = true;
        public string  MonthlyBudget { get; set; } = "0";

        public string FormattedMonthlyBudget {get; set;}


      


        public static SettingsModel LoadSettings()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullPath = System.IO.Path.Combine(folderPath, FilePath);

            if (File.Exists(fullPath))
            {
                try
                {
                    string json = File.ReadAllText(fullPath);
                    var settingsDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    if (settingsDict != null)
                    {
                        return new SettingsModel
                        {
                            IsDarkMode = bool.TryParse(settingsDict.GetValueOrDefault("IsDarkMode"), out bool darkMode) && darkMode,
                            Language = settingsDict.ContainsKey("Language") ? settingsDict["Language"] : "en",
                            NotificationsEnabled = bool.TryParse(settingsDict.GetValueOrDefault("NotificationsEnabled"), out bool notif) && notif,
                            MonthlyBudget = settingsDict.ContainsKey("MonthlyBudget") ? settingsDict["MonthlyBudget"] : "0"
                        };
                    }
                }
                catch
                {
                    // Handle any exceptions that may occur during file reading or parsing.
                }
            }
            return new SettingsModel();
        }

        public void SaveSettings()
        {

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullPath = System.IO.Path.Combine(folderPath, FilePath);

            var settingsDict = new Dictionary<string, string>
            {
                { "IsDarkMode", IsDarkMode.ToString() },
                { "Language", Language },
                { "NotificationsEnabled", NotificationsEnabled.ToString() },
                { "MonthlyBudget", MonthlyBudget }
            };

            string json = JsonSerializer.Serialize(settingsDict);
            File.WriteAllText(fullPath, json);



        }


      




    }
}
