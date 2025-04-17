using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        private ReportsViewModel reportObj;
        private SettingsModel settingsModel; // Use the separated model
        public string MonthlyBudgetLabelText { get; set; } = "Monthly Budget: ";


        public MainPage()
        {
            InitializeComponent();

            reportObj = new ReportsViewModel();
            DisplayChart.Chart = reportObj.LoadRandomChart();

            BindingContext = App.SharedSettingsViewModel;
        }



        public void OnSwipedLeft(object sender, EventArgs e)
        {
            DisplayChart.Chart = reportObj.LoadRandomChart();
        }
    }
}
