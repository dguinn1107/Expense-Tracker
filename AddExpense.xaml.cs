using System.Collections.ObjectModel;

namespace ExpenseTracker;

public partial class AddExpense : ContentPage
{
    private Settings _settings;

    private ObservableCollection<string> transactions = new ObservableCollection<string>(); //collection of strings

    //private CollectionView TransactionList;


    public AddExpense()
    {
        InitializeComponent();
        LoadTransactions();
        TransactionList.ItemsSource = transactions;
        
    }

    private async void OnAddExpenseClicked(object sender, EventArgs e) //*should open an expense pop-up/form for the user to fill out*
    {
        //    var expense = new Expense
        //    {
        //        Month = DateTime.Now.ToString("MMMM"),
        //        Amount = float.Parse(ExpenseAmountEntry.Text),
        //        Category = ExpenseCategoryEntry.Text
        //    };
        //    if (Expense.Amount < Reports.returnMonthlyBudget)
        //    {

        //    }
        //    else
        //    {
        //    }
        // ^^ !! Your logic for the button that I commented out in the .xaml of this page!! ^^
        var addExpenseItemPage = new AddExpenseItem();
        addExpenseItemPage.ExpenseAdded += (name, amount) =>
        {
            string expenseName = name;
            string expenseAmount = "$" + amount;
            string expenseEntry = $"Type: {name} \nAmount: ${amount}\nDate: {DateTime.Now:yyyy-MM-dd}"; //im on this step       
                                                                                                        //transactions.Add(expenseEntry);
            transactions.Insert(0, expenseEntry);//save in csv at the first index so oldest (will display at top of list) 

            TransactionList.ItemsSource = null; //to clear or refresh 
            TransactionList.ItemsSource = transactions; //updates the list

        };
        await Navigation.PushModalAsync(addExpenseItemPage); //
    }

    private void LoadTransactions() // should display the transactions in the "AddExpense" tab of the application
    {
        //get path of csv file/D helped
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "expenses.csv");

        if (File.Exists(filePath)) //read the lines except the first because that's a header row
        {
            var lines = File.ReadAllLines(filePath).Skip(1).Reverse();//creates a defined "lines" object from csv and should
                                                                      //provide data from newest to oldest

            //for (int i = 1; i < lines.Length; i++) would iterate through each line and then display
            //(not what we want because it displays from oldest to newest)
            //{
            //transactions.Add(lines[i]);
            //}
            foreach (var line in lines) //iterates through the lines in csv and cuts into pieces assigning
                                        //each to corresponding value then displays existing data as formatted
            {
                var parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    var type = parts[1];
                    var date = parts[2];
                    var amount = parts[0];


                    //formatting part
                    string formattedEntry = $"Type: {type}\nAmount: ${amount}\nDate: {date}";
                    //transactions.Insert(0, formattedEntry);
                    transactions.Add(formattedEntry); // load csv regularly so from newest to oldest
                }

            }
        }
    }
}