using Microcharts;
using SkiaSharp;
using System.Globalization;

namespace ExpenseTracker;


public partial class Reports : ContentPage
{
   

    public Reports()
    {
        InitializeComponent();
        LoadCharts();
    }

   
    public void LoadCharts()
    {

        var expenses = Expense.CurrentExpenses();


        var categorySums = expenses
            .Where(e => !string.IsNullOrEmpty(e.Category))
            .GroupBy(e => e.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
            .ToList();

        var entries = categorySums.Select(g => new ChartEntry((float)g.Total)
        {
            Label = g.Category,
            ValueLabel = $"${g.Total}",
            Color = SKColor.Parse(GetRandomColor())
        }).ToList();

        PieChartView.Chart = new PieChart { Entries = entries };
        BarChartView.Chart = new BarChart { Entries = entries };
        LineChartView.Chart = new LineChart { Entries = entries };
        DonutChart.Chart = new DonutChart { Entries = entries };
        RadarChartView.Chart = new RadarChart { Entries = entries };
    }

    private string GetRandomColor()
    {
        var random = new Random();
        return $"#{random.Next(0x1000000):X6}"; // random hex color
    }

    public PieChart pieChart()
    {
        var expenses = Expense.CurrentExpenses();
        var categorySums = expenses
            .Where(e => !string.IsNullOrEmpty(e.Category))
            .GroupBy(e => e.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
            .ToList();
        var entries = categorySums.Select(g => new ChartEntry((float)g.Total)
        {
            Label = g.Category,
            ValueLabel = $"${g.Total}",
            Color = SKColor.Parse(GetRandomColor())
        }).ToList();

        var pieChart = new PieChart { Entries = entries }; // Create a new PieChart instance
        PieChartView.Chart = pieChart; // Assign it to the PieChartView.Chart property
        return pieChart; // Return the created PieChart instance
    }


    public Chart LoadRandomChart()
    {
        var expenses = Expense.CurrentExpenses();
        var categorySums = expenses
            .Where(e => !string.IsNullOrEmpty(e.Category))
            .GroupBy(e => e.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
            .ToList();

        var entries = categorySums.Select(g => new ChartEntry((float)g.Total)
        {
            Label = g.Category,
            ValueLabel = $"${g.Total}",
            Color = SKColor.Parse(GetRandomColor())
        }).ToList();

        var randomChart = new Random();
        int randomIndex = randomChart.Next(0, 4);
        Chart chart = null;

        switch (randomIndex)
        {
            case 0:
                chart = new PieChart { Entries = entries };
                break;
            case 1:
                chart = new BarChart { Entries = entries };
                break;
            case 2:
                chart = new LineChart { Entries = entries };
                break;
            case 3:
                chart = new DonutChart { Entries = entries };
                break;
        }

        return chart; // Ensure a Chart is always returned  
    }
}



   



