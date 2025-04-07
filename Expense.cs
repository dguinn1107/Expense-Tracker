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
        public string Month { get; set; }
        public float Amount { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }

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


        public static List<Expense> CurrentExpenses()
        {
            // DUMMY DATA FOR TESTING
            return new List<Expense>
                {
                    new Expense { Month = "January", Amount = 200, Category = "Healthcare" },
                    new Expense { Month = "February", Amount = 400, Category = "Other" },
                    new Expense { Month = "Jnauary", Amount = 150, Category = "Transportation" },
                    new Expense { Month = "April", Amount = 300 },
                    new Expense { Month = "May", Amount = 500 , Category = "Food"}
                };
        }
    }
}
