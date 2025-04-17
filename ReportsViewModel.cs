using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microcharts;
using SkiaSharp;

namespace ExpenseTracker
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _maxMonth;
        private int _monthSliderValue = DateTime.Now.Month; // default to current month
        public int MaxMonth
        {
            get => _maxMonth;
            set
            {
                _maxMonth = value;
                OnPropertyChanged(nameof(MaxMonth));
            }
        }


        public int MaxAvailableMonth { get; set; } = DateTime.Now.Month;

        public int MonthSliderValue
        {
            get => _monthSliderValue;
            set
            {
                if (_monthSliderValue != value)
                {
                    _monthSliderValue = value;
                    OnPropertyChanged(nameof(MonthSliderValue));
                    OnPropertyChanged(nameof(MonthSliderLabelText));
                    LoadCharts();
                }
            }
        }

        public string ReportLabelText => "Monthly Report";

        public string MonthSliderLabelText => $"Showing reports for: {GetMonthName(MonthSliderValue)}";

        public Chart PieChart { get; set; }
        public Chart BarChart { get; set; }
        public Chart LineChart { get; set; }
        public Chart DonutChart { get; set; }

        public ReportsViewModel()
        {
            LoadCharts();

            var latestMonth = Expense.CurrentExpenses()
                         .Select(e => e.Date.Month)
                         .DefaultIfEmpty(DateTime.Now.Month)
                         .Max();

            MaxMonth = latestMonth;

        }


        public Chart LoadRandomChart()
        {
            var expenses = Expense.CurrentExpenses(); 
            var random = new Random();

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

            var chartType = random.Next(0, 4); 

            switch (chartType)
            {
                case 0:
                    return new PieChart { Entries = entries };
                case 1:
                    return new BarChart { Entries = entries };
                case 2:
                    return new LineChart { Entries = entries };
                case 3:
                    return new DonutChart { Entries = entries };
                default:
                    return new PieChart { Entries = entries }; 
            }
        }



        private void LoadCharts()
        {
            var expenses = Expense.CurrentExpenses()
                .Where(e => e.Date.Month == MonthSliderValue && !string.IsNullOrEmpty(e.Category))
                .GroupBy(e => e.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();

            var entries = expenses.Select(g => new ChartEntry((float)g.Total)
            {
                Label = g.Category,
                ValueLabel = $"${g.Total}",
                Color = SKColor.Parse(GetRandomColor())
            }).ToList();

            PieChart = new PieChart { Entries = entries };
            BarChart = new BarChart { Entries = entries };
            LineChart = new LineChart { Entries = entries };
            DonutChart = new DonutChart { Entries = entries };

            OnPropertyChanged(nameof(PieChart));
            OnPropertyChanged(nameof(BarChart));
            OnPropertyChanged(nameof(LineChart));
            OnPropertyChanged(nameof(DonutChart));
        }

        private string GetMonthName(int month)
        {
            return new DateTime(1, month, 1).ToString("MMMM");
        }

        private string GetRandomColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}";
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
