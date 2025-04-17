using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

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
            // Load existing settings from file or use defaults.
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
                    _settings.SaveSettings();
                    UpdateTheme(); // Apply theme changes
                }
            }
        }

        private void UpdateTheme()
        {
            if (_settings.IsDarkMode)
            {
                Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#303030");
                Application.Current.Resources["AppTextColor"] = Color.FromArgb("#FFFFFF");
                // Adjust other resources as needed.
            }
            else
            {
                Application.Current.Resources["AppBackgroundColor"] = Color.FromArgb("#FFFFFF");
                Application.Current.Resources["AppTextColor"] = Color.FromArgb("#303030");
                // Adjust other resources as needed.
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
                    _settings.SaveSettings();
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
                    _settings.SaveSettings();
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
                    ValidateMonthlyBudget();
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

        private void ValidateMonthlyBudget()
        {
            // Check for empty input
            if (string.IsNullOrEmpty(MonthlyBudget))
            {
                MonthlyBudgetError = "Please enter a valid budget";
                return;
            }
            // Check if value is a valid decimal number
            if (!decimal.TryParse(MonthlyBudget, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal budget))
            {
                MonthlyBudgetError = "Invalid number format";
                return;
            }
            // Ensure the budget is non-negative
            if (budget < 0)
            {
                MonthlyBudgetError = "Budget cannot be negative";
                return;
            }
            // Check the length of the input
            if (MonthlyBudget.Length > 10)
            {
                MonthlyBudgetError = "Budget too long";
                return;
            }
            // All validations passed.
            MonthlyBudgetError = string.Empty;
            _settings.MonthlyBudget = MonthlyBudget;
            _settings.SaveSettings();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
