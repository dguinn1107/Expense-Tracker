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
            
            MonthlyBudgetEntry.Text = e.NewTextValue;
            
        }

        private void SaveSettingsButton_Clicked(object sender, EventArgs e)
        {
            var Model = BindingContext as SettingsModel;
            if (Model != null)
            {
                Model.SaveSettings();

                Model.ReloadMainPageLabel();
                DisplayAlert("Success", "Settings saved successfully.", "OK");
            }
        }
    }
}

