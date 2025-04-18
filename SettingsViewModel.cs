using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsModel _settings;
        private string _monthlyBudget;
        private string _monthlyBudgetError;
       

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            // Load existing settings from the model
            _settings = SettingsModel.LoadSettings();
            MonthlyBudget = _settings.MonthlyBudget;
            IsDarkMode = _settings.IsDarkMode;
            Language = _settings.Language;
            NotificationsEnabled = _settings.NotificationsEnabled;
        }

    

        public bool IsDarkMode
        {
            get => _settings.IsDarkMode;
            set
            {
                if (_settings.IsDarkMode != value)
                {
                    _settings.IsDarkMode = value;
                    OnPropertyChanged();
                    // No immediate theme update; will update only on save
                }
            }
        }

        public string Language
        {
            get => _settings.Language;
            set
            {
                if (_settings.Language != value)
                {
                    _settings.Language = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NotificationsEnabled
        {
            get => _settings.NotificationsEnabled;
            set
            {
                if (_settings.NotificationsEnabled != value)
                {
                    _settings.NotificationsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MonthlyBudget
        {
            get => _monthlyBudget;
            set
            {
                if (_monthlyBudget != value)
                {
                    _monthlyBudget = value;
                    OnPropertyChanged();
                  
                }
            }
        }

        public string MonthlyBudgetError
        {
            get => _monthlyBudgetError;
            private set
            {
                if (_monthlyBudgetError != value)
                {
                    _monthlyBudgetError = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ValidateMonthlyBudget(string userBudget)
        {
            if (string.IsNullOrEmpty(userBudget))
            {
                MonthlyBudgetError = "Please enter a valid budget";
                return false;
            }
            if (!decimal.TryParse(userBudget, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal budget))
            {
                MonthlyBudgetError = "Invalid number format";
                return false;
            }
            if (budget <= 0)
            {
                MonthlyBudgetError = "Budget cannot be negative";
                return false;
            }
            if (userBudget.Length > 10)
            {
                MonthlyBudgetError = "Budget too long";
                return false;
            }
            MonthlyBudgetError = string.Empty;
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void SaveSettings()
        {
            Debug.WriteLine($"Current MonthlyBudget: {MonthlyBudget}");
            if (ValidateMonthlyBudget(MonthlyBudget))
            {
                _settings.MonthlyBudget = MonthlyBudget;
                _settings.SaveSettings();
            }
            else
            {
                Debug.WriteLine("Invalid Monthly Budget.");
            }
        }

        public void LoadSettings()
        {
            _settings = SettingsModel.LoadSettings();
            MonthlyBudget = _settings.MonthlyBudget;
            IsDarkMode = _settings.IsDarkMode;
            Language = _settings.Language;
            NotificationsEnabled = _settings.NotificationsEnabled;
        }

        public void UpdateTheme()
        {
            if (_settings.IsDarkMode)
            {
                Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#345D7E");
                Application.Current.Resources["AppTextColor"] = Color.FromArgb("#FFFFFF");
            }
            else
            {
                Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#FFFFFF");
                Application.Current.Resources["AppTextColor"] = Color.FromArgb("#000000");
            }
        }

        public void ApplySettings()
        {
            // Validate and then save settings.
            if (ValidateMonthlyBudget(MonthlyBudget))
            {
                _settings.MonthlyBudget = MonthlyBudget;
                _settings.SaveSettings();
            }
            else
            {
                Debug.WriteLine("Invalid Monthly Budget.");
            }

            // Update the global theme based on the saved IsDarkMode value.
            UpdateTheme();
        }
    }
}

