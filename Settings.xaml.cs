
using System.IO;

namespace ExpenseTracker;

public partial class Settings : ContentPage
{

    public Settings()
    {
        InitializeComponent();
        LoadSettings();
        UpdateTheme();
    }

    private const string FilePath = "settings.txt";

        private bool isDarkMode;

    public Settings(bool isDarkMode)
    {
        this.isDarkMode = isDarkMode;
        InitializeComponent();
        LoadSettings();
        UpdateTheme();
    }

    public bool IsDarkMode
        {
            get { return isDarkMode; } 
            set
            {
                isDarkMode = value;
                SaveSettings(); 
            }
        }




    private void IsDarkModeEnabled(object sender, ToggledEventArgs e)
    {
        this.IsDarkMode = e.Value;
        UpdateTheme();
        SaveSettings();
    }

    private void UpdateTheme()
    {
        if (IsDarkMode)
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromHex("#4CAF50");
        }
        else
        {
            Application.Current.Resources["AppBackgroundColor"] = Color.FromHex("#FFFFFF");
        }
    }


    private void LoadSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, FilePath);

        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            bool.TryParse(fileContent, out isDarkMode); 
        }
        else
        {
            isDarkMode = false; 
        }
    }

    private void SaveSettings()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, FilePath);

        File.WriteAllText(filePath, isDarkMode.ToString());
    }

}

