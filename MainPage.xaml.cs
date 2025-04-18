using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        private ReportsViewModel reportObj;
        private SettingsViewModel _viewModel;
        


        public MainPage()
        {
            InitializeComponent();

            reportObj = new ReportsViewModel();
            DisplayChart.Chart = reportObj.LoadRandomChart();

            //_viewModel = new SettingsViewModel();
            //_viewModel.LoadSettings();



        }



        public void OnSwipedLeft(object sender, EventArgs e)
        {
            DisplayChart.Chart = reportObj.LoadRandomChart();
        }
    }
}
