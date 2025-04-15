using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
        }
    }
}
