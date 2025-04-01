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

            // Fetch dummy data
            var data = Reports.CurrentExpenses();

            // Convert dummy data to Microcharts entries
            var chartEntries = data.Select(expense => new Microcharts.ChartEntry(expense.Amount)
            {
                Label = expense.Month,
                ValueLabel = expense.Amount.ToString(),
                Color = SKColor.Parse("#0000FF") 
            }).ToArray();

            // Set chart data to the ChartView
            CurrentExpensesChartView.Chart = new BarChart { Entries = chartEntries };
        }

        public void OnSettingsClicked(object sender, EventArgs e)
        {
            
        }
    }

}


