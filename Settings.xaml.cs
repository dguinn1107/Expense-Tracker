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

        private void MonthlyBudgetEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update the MonthlyBudget property in the ViewModel
            var viewModel = BindingContext as SettingsViewModel;
            if (viewModel != null)
            {
                viewModel.MonthlyBudget = e.NewTextValue;
            }
        }

        private void SaveSettingsButton_Clicked(object sender, EventArgs e)
        {
            
            var viewModel = BindingContext as SettingsViewModel;
            if (viewModel != null)
            {
                
                viewModel.SaveSettings();
                DisplayAlert("Success", "Settings saved successfully.", "OK");
            }
        }
    }
}

  

