using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class Settings : ContentPage
    {
        private SettingsViewModel _viewModel;
        public Settings()
        {
            InitializeComponent();

            _viewModel = new SettingsViewModel();
            _viewModel.LoadSettings();
            BindingContext = _viewModel;
        }

        private void MonthlyBudgetEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update MonthlyBudget in ViewModel when text changes
            _viewModel.MonthlyBudget = e.NewTextValue;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _viewModel.ApplySettings();

            MonthlyBudgetEntry.Text = null; 
            DisplayAlert("Success", "Settings saved successfully.", "OK");
        }
    }
}
