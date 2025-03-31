namespace ExpenseTracker;

public partial class Reports : ContentPage
{
	public Reports()
	{
		InitializeComponent();
	}


    public class Expense
    {
        public string Month { get; set; }
        public float Amount { get; set; }
    }

    public static List<Expense> CurrentExpenses()
    {
        // DUMMY DATA FOR TESTING
        return new List<Expense>
        {
            new Expense { Month = "January", Amount = 200 },
            new Expense { Month = "February", Amount = 400 },
            new Expense { Month = "March", Amount = 150 },
            new Expense { Month = "April", Amount = 300 }
        };
    }
}

