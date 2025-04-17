using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Expense
    {
        public float Amount { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } 

        private List<string> Categories = new List<string>
    {
        "Food", "Transportation", "Entertainment", "Utilities", "Healthcare", "Other"
    };

        public string _Category
        {
            get => Category;
            set
            {
                if (string.IsNullOrEmpty(value) || !Categories.Contains(value))
                {
                    throw new ArgumentException("Invalid or missing category.");
                }
                Category = value;
            }
        }

        public static List<Expense> CurrentExpenses()
        {
            var expenses = new List<Expense>();
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = System.IO.Path.Combine(folderPath, "expenses.csv");

            if (!File.Exists(filePath)) return expenses;

            var lines = File.ReadAllLines(filePath).Skip(1);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    if (float.TryParse(parts[0], out float parsedAmount)
                        && DateTime.TryParse(parts[2], out DateTime parsedDate))
                    {
                        var entry = new Expense
                        {
                            Amount = parsedAmount,
                            Category = parts[1],
                            Date = parsedDate,
                            Name = parts.Length > 3 ? parts[3] : ""
                        };
                        expenses.Add(entry);
                    }
                }
            }

            return expenses;
        }

        public static float TotalAmountOfExpenses()
        {
            return CurrentExpenses().Sum(e => e.Amount);
        }

        public static float ExceedsMonthlyBudget()
        {
            var settingsModel = new SettingsModel();
            return TotalAmountOfExpenses() - float.Parse(settingsModel.MonthlyBudget);
        }
    }

}
