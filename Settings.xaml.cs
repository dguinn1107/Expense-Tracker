using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace ExpenseTracker;

public partial class Settings : ContentPage, INotifyPropertyChanged
{
    private const string FilePath = "settings.json";
    private bool isDarkMode;
    private string language = "en"; // Default language
    private bool notificationsEnabled; // Default notification preference

    public event PropertyChangedEventHandler PropertyChanged;

    public Settings()
    {
        InitializeComponent();
        BindingContext = this;  // Set the BindingContext for data binding.
        LoadSettings();
        UpdateTheme();
        //UpdateLanguage();
    }

    public bool IsNotificationsEnabled
    {
        get => notificationsEnabled;
        set
        {
            if (notificationsEnabled != value)
            {
                notificationsEnabled = value;
                OnPropertyChanged(nameof(IsNotificationsEnabled));
                SaveSettings();
            }
        }
    }

    public bool IsDarkMode
    {
        get => isDarkMode;
        set
        {
            if (isDarkMode != value)
            {
                isDarkMode = value;
                OnPropertyChanged(nameof(IsDarkMode));
                SaveSettings();
                UpdateTheme();
            }
        }
    }

    public string Language
    {
        get => language;
        set
        {
            if (language != value)
            {
                language = value;
                OnPropertyChanged(nameof(Language));
                SaveSettings();
                UpdateLanguage();
            }
        }
    }

    private void IsDarkModeEnabled(object sender, ToggledEventArgs e)
    {
        IsDarkMode = e.Value;
    }

    private void OnLanguageSelected(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedItem is string selectedLanguage)
        {
            Language = selectedLanguage;
        }
    }

    private void UpdateTheme()
    {
        if (IsDarkMode)
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#DDE6F1");
        }
        else
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#FFFFFF");
        }
    }

    private void UpdateLanguage()
    {
        // Implement your language update logic here
    }

    public void LoadSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var fullPath = Path.Combine(folderPath, FilePath);

        if (File.Exists(fullPath))
        {
            try
            {
                string json = File.ReadAllText(fullPath);
                var settingsDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (settingsDict != null)
                {
                    if (settingsDict.TryGetValue("IsDarkMode", out string darkModeValue))
                        bool.TryParse(darkModeValue, out isDarkMode);
                    if (settingsDict.TryGetValue("Language", out string lang))
                        language = lang;
                    if (settingsDict.TryGetValue("NotificationsEnabled", out string notifValue))
                        bool.TryParse(notifValue, out notificationsEnabled);

                    // After loading, notify the UI that properties have changed.
                    OnPropertyChanged(nameof(IsDarkMode));
                    OnPropertyChanged(nameof(Language));
                    OnPropertyChanged(nameof(IsNotificationsEnabled));
                }
            }
            catch
            {
                // If deserialization fails, keep defaults.
                isDarkMode = false;
                language = "en";
                notificationsEnabled = true;
            }
        }
        else
        {
            // Set default settings if file doesn't exist.
            isDarkMode = false;
            language = "en";
            notificationsEnabled = true;
        }
    }

    public void SaveSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var fullPath = Path.Combine(folderPath, FilePath);

        var settingsDict = new Dictionary<string, string>
        {
            { "IsDarkMode", isDarkMode.ToString() },
            { "Language", language },
            { "NotificationsEnabled", notificationsEnabled.ToString() }
        };

        string json = JsonSerializer.Serialize(settingsDict);
        File.WriteAllText(fullPath, json);
    }

    private void OnNotificationsToggled(object sender, ToggledEventArgs e)
    {
        IsNotificationsEnabled = e.Value;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
