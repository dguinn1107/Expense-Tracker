using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        private Reports reportObj;
        private SettingsModel settingsModel; // Use the separated model
        

        public MainPage()
        {
            InitializeComponent();

            reportObj = new Reports();
            DisplayChart.Chart = reportObj.LoadRandomChart();

            settingsModel = SettingsModel.LoadSettings();
         



            MonthlyBudgetLabel.Text = $"Monthly Budget: {settingsModel.MonthlyBudget}";
        }

        public void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }

        public void OnSwipedLeft(object sender, EventArgs e)
        {
            DisplayChart.Chart = reportObj.LoadRandomChart();
        }
    }
}
