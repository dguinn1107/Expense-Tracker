using Microcharts;
using SkiaSharp;
using System.Globalization;

namespace ExpenseTracker;

public partial class Reports : ContentPage
{

    public static decimal MonthlyBudget { get; set; }

    public string MonthlyBudgetEntryText
    {
        get => MonthlyBudgetEntry.Text;
        set => MonthlyBudgetEntry.Text = value;
    }

    public Reports()
	{
		InitializeComponent();
        LoadCharts();

    }

    private void MonthlyBudgetEntry_BindingContextChanged(object sender, EventArgs e)
    {
        ValidateMonthlyBudget();


    }


    private void ValidateMonthlyBudget()
    {
        var monthlyBudget = MonthlyBudgetEntry.Text;

        if (string.IsNullOrEmpty(monthlyBudget))
        {
            MonthlyBudgetEntry.TextColor = Colors.Red;
            MonthlyBudgetEntry.Placeholder = "Please enter a valid budget";
        }
        else if (!decimal.TryParse(monthlyBudget, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal budget))
        {
            MonthlyBudgetEntry.TextColor = Colors.Red;
            MonthlyBudgetEntry.Placeholder = "Invalid number format";
        }
        else if (budget < 0)
        {
            MonthlyBudgetEntry.TextColor = Colors.Red;
            MonthlyBudgetEntry.Placeholder = "Budget cannot be negative";
        }
        else if (monthlyBudget.Length > 10)
        {
            MonthlyBudgetEntry.TextColor = Colors.Red;
            MonthlyBudgetEntry.Placeholder = "Budget too long";
        }
        else
        {
            MonthlyBudgetEntry.TextColor = Colors.Black;
            MonthlyBudgetEntry.Placeholder = "Enter your monthly budget";
        }
    }

    public string returnMonthlyBudget()
    {
        return MonthlyBudgetEntry.Text;
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

    public Chart pieChart()
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

        return PieChartView.Chart = new PieChart { Entries = entries };
    }

    public Chart barChart()
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

        return BarChartView.Chart = new BarChart { Entries = entries };
    }
    public Chart lineChart()
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
        return LineChartView.Chart = new LineChart { Entries = entries };
    }
    public Chart donutChart()
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
        return DonutChart.Chart = new DonutChart { Entries = entries };
    }
    public Chart radarChart()
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
        return RadarChartView.Chart = new RadarChart { Entries = entries };
    }
    private string GetRandomColor()
    {
        var random = new Random();
        return $"#{random.Next(0x1000000):X6}"; //  random hex color
    }
}

