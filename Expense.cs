﻿using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Expense //might have to change to "public static class Expense" to be able to use method
    {
        public string Month { get; set; } //subject to deletion or to be replaced by variable "date" in comment below 
        public float Amount { get; set; } //will be used to create object in Method for loading csv 
        public string Category { get; set; } //will be used to create object in Method for loading csv 
        public string Name { get; set; }

        //public DateTime date {get; set;} //a new variable that will be used in Method; not entirely sure
        // **how to strictly extract the month from date that was submitted to csv**

        private List<string> Categories = new List<string>
            {
                "Food",
                "Transportation",
                "Entertainment",
                "Utilities",
                "Healthcare",
                "Other"
            };

        public string _Category
        {
            get { return Category; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Category cannot be null or empty");
                }

                if (!Categories.Contains(value))
                {
                    throw new ArgumentException($"Invalid category. Valid categories are: {string.Join(", ", Categories)}");
                }
                else
                {
                    Category = value;
                }
            }
        }
        //public

        public static List<Expense> CurrentExpenses()
        {
            var expenses = new List<Expense>(); // creates empty list
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // existing file will be stored in variable
            var filePath = System.IO.Path.Combine(folderPath, "expenses.csv");

            if (!File.Exists(filePath)) return expenses; // returns an empty list if file doesn't exist

            var lines = File.ReadAllLines(filePath).Skip(1); // read all lines except the first as it's a header
            foreach (var line in lines) // reusing the foreach from AddExpense.xaml.cs
            {
                var parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    var category = parts[1]; // "type" was renamed Category (existing variable in this same cs page)
                    var date = parts[2];
                    var amount = parts[0];

                    if (float.TryParse(amount, out float parsedAmount)) // safely parse the amount
                    {
                        var entry = new Expense { Amount = parsedAmount, Category = category }; // creates object
                        expenses.Add(entry); // load csv regularly and adds object into expenses
                    }
                }
            }

            return expenses; // ensures all code paths return a value
        }

        public static float TotalAmountOfExpenses ()
        {
            var expenses = CurrentExpenses();
            float totalAmount = expenses.Sum(e => e.Amount);
            return totalAmount;
        }

        public static float ExceedsMonthlyBudget()
        {
            var settingsModel = new SettingsModel();

            var expenses = CurrentExpenses();
            float totalAmount = expenses.Sum(e => e.Amount);
            return totalAmount - float.Parse(settingsModel.MonthlyBudget);
        }
    }
}
