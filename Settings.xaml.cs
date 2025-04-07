using Microsoft.Maui.Controls;
using System.IO;

namespace ExpenseTracker;

public partial class Settings : ContentPage
{
    private const string FilePath = "settings.txt";
    private bool isDarkMode;
    private string language = "en"; // Default language
    private bool notificationsEnabled; // Default notification preference

    public Settings()
    {
        InitializeComponent();
        LoadSettings();
        UpdateTheme();
        //UpdateLanguage();
    }

    public bool IsNotificationsEnabled
    {
        get { return notificationsEnabled; }
        set
        {
            notificationsEnabled = value;
            SaveSettings();
        }
    }

    public bool IsDarkMode
    {
        get { return isDarkMode; }
        set
        {
            isDarkMode = value;
            SaveSettings();
            UpdateTheme();
        }
    }

    public string Language
    {
        get { return language; }
        set
        {
            language = value;
            SaveSettings();
            UpdateLanguage();
        }
    }

    private void IsDarkModeEnabled(object sender, ToggledEventArgs e)
    {
        this.IsDarkMode = e.Value;
    }

    private void OnLanguageSelected(object sender, EventArgs e)
    {
        var selectedLanguage = (sender as Picker).SelectedItem.ToString();
        this.Language = selectedLanguage;
    }

    private void UpdateTheme()
    {
        if (IsDarkMode)
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#4CAF50");
        }
        else
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#FFFFFF");
        }
    }

    private void UpdateLanguage()
    {

    }

    private void LoadSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, FilePath);

        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllLines(filePath);
            if (fileContent.Length > 0)
            {
                bool.TryParse(fileContent[0], out isDarkMode);
            }
            if (fileContent.Length > 1)
            {
                language = fileContent[1];
            }
            if (fileContent.Length > 2)
            {
                bool.TryParse(fileContent[2], out notificationsEnabled);
            }
        }
        else
        {
            isDarkMode = false;
            language = "en";
            IsNotificationsEnabled = true; // Default notification preference
        }
    }

    private void SaveSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, FilePath);

        var settings = new string[] { isDarkMode.ToString(), language, notificationsEnabled.ToString() };
        File.WriteAllLines(filePath, settings);
    }

    private void OnNotificationsToggled(object sender, ToggledEventArgs e)
    {
        // Handle notifications toggle
        bool isEnabled = e.Value;
        // Save the notification preference
        IsNotificationsEnabled = isEnabled;
    }
}
