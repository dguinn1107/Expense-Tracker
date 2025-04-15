using Microsoft.Maui.Controls.Shapes;
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

        public static List<Expense> CurrentExpenses() //**will simply re-use this method since it seems to be same**
                                                      //functionality but now it will load data from csv not just establish data
        {
            // var expenses = new List<Expense>(); //creates empty list
            //var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //existing file will be stored in variable
            //var filePath = Path.Combine(folderPath, "expenses.csv"); //
            //
            // if (!File.Exists(filePath)) return expenses; //returns an aempty list if file doesnt exist

            //var lines = File.ReadAllLines(filePath).Skip(1); //read all lines except first as its a header
            //foreach (var line in lines) // reusing the foreach from AddExpense.xaml.cs
            //{
            //var parts = line.Split(',');

            //if (parts.Length >= 3) 
            //{
            //var Category = parts[1]; // "type" was renamed Category(existing variable in this same cs page)
            //var date = parts[2];
            //var amount = parts[0];
            //
            //var entry = new Expense { Amount = (float)amount, Category = category }; //creates object
            //expenses.Add(entry); // load csv regularly and adds object into expenses
            //}


            // DUMMY DATA FOR TESTING
            return new List<Expense> //might have to change to "return expenses;" and delete code from below
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
