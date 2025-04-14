namespace ExpenseTracker;

public partial class AddExpenseItem : ContentPage
{
    public event Action<string, decimal> ExpenseAdded;

    public AddExpenseItem()
	{
		InitializeComponent();
        BindingContext = new AddExpenseItemViewModel();
    }

    private void SaveToCsv(string expenseAmount, string expenseType, DateTime expenseDate) //creates new csv file
    {
        //define the file path                                    
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "expenses.csv");

        // Display path just to confirm its being saved during testing
        //Console.WriteLine("CSV Path: " + filePath);
        //Application.Current.MainPage.DisplayAlert("Saving", $"Path:\n{filePath}", "OK"); //testing

        if (!File.Exists(filePath)) //if file doesnt exist; create new one
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ExpenseAmount,ExpenseType,ExpenseDate"); // Header for file
            }
        }
        // add new data to csv
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine($"{expenseAmount},{expenseType},{expenseDate:yyyy-MM-dd}"); // properties
        }
        //confirms save visually (for testing)
        //Application.Current.MainPage.DisplayAlert("Saved", "Expense was saved successfully.", "OK");

    }




    private async void OnSaveExpense(object sender, EventArgs e) //saving properties
    {
        var viewModel = (AddExpenseItemViewModel)BindingContext;

        string expenseAmount = viewModel._ExpenseAmount;
        string selectedExpenseType = viewModel._SelectedExpenseType;
        DateTime selectedDate = viewModel._SelectedDate;

        decimal amount; //the vairable im defing so that the "out amount can be used"

        //if expense amount and or Type fields are empty or invalid; message pops up to notify user
        if (string.IsNullOrEmpty(expenseAmount) || string.IsNullOrEmpty(selectedExpenseType) ||
    !decimal.TryParse(expenseAmount, out amount) ||
    (expenseAmount.Contains('.') && expenseAmount.Split('.')[1].Length > 2))
        {
            await DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
            return;
        }

        SaveToCsv(expenseAmount, selectedExpenseType, selectedDate);

        //lets AddExpense page know if needed; npotifies AdExpense page        
            ExpenseAdded?.Invoke(selectedExpenseType, amount);
 
        await DisplayAlert("Saved", "Your expense has been saved.", "OK");//  "Saved" confirmation
    
        await Navigation.PopModalAsync(); // goes back to AddExpense page
    }
    private async void OnCancel(object sender, EventArgs e)//confirmation before cancelling
    {
        
        bool Cancel = await DisplayAlert("Cancel", "Are you sure you want to cancel?", "Yes", "No");
        if (Cancel)
        {
            await Navigation.PopModalAsync(); // Go back to AddExpense after user confirms to cancel
        }
    }
}