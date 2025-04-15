using Microcharts.Maui;
using SkiaSharp;  
using Microsoft.Maui.Controls;
using Microcharts;




namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var reportObj = new Reports();


            DisplayChart.Chart = reportObj.pieChart();

        }

        public void OnSettingsClicked(object sender, EventArgs e)
        {
        }
    }

}


