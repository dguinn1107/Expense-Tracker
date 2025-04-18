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

        addExpenseItemPage.ExpenseAdded += (type, name, amount) =>
        {
            string expenseName = name;
            string expenseAmount = "$" + amount;
            string expenseEntry = $"Name: {name}\nType: {type}\nAmount: ${amount}\nDate: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"; //im on this step       
                                                                                                        //transactions.Add(expenseEntry);
            transactions.Insert(0, expenseEntry);//save in csv at the first index so oldest (will display at top of list) 

            //save to CSV file so it persists after restart
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = Path.Combine(folderPath, "expenses.csv");

            // If file doesn't exist, create it and add headers
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Amount,Type,Date,Name\n");
            }

            // Append this new transaction to the CSV file
            //var csvLine = $"{amount},{type},{DateTime.Now:yyyy-MM-dd},{name}"; //not necessary, was duplicating lates expense
            //File.AppendAllText(filePath, csvLine + "\n"); //not necessary, was duplicating latest expense


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

                if(parts.Length == 3) //loads the old transactions (transactions that didn't have a name property)
                {
                    var type = parts[0];
                    var amount = parts[1];
                    var date = parts[2];
                    string formattedEntry = $"Name: N/A\nType: {type}\nAmount: ${amount}\nDate: {date}";
                    transactions.Add(formattedEntry);
                }

                else if (parts.Length >= 4)
                {
                    var type = parts[2]; //0000
                    var date = parts[3]; //3333
                    var amount = parts[1]; //1111
                    var name= parts[0]; //22


                    //formatting part
                    string formattedEntry = $"Name: {name}\nType: {type}\nAmount: ${amount}\nDate: {date}";
                    //transactions.Insert(0, formattedEntry);
                    transactions.Add(formattedEntry); // load csv regularly so from newest to oldest
                }

            }
        }
    }

    private async void DeleteExpenseClicked(object sender, EventArgs e)//method for deletion once user clicks on delete button in xaml
    {
        if (sender is Button deleteButton && deleteButton.CommandParameter is string transactionToDelete)
        {
            bool confirm = await DisplayAlert("Delete Expense",
                "Are you sure you want to delete this expense?",
                "Yes", "No");

            if (confirm)
            {
                transactions.Remove(transactionToDelete); //remove from ObservableCollection

                //remove from csv
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var filePath = Path.Combine(folderPath, "expenses.csv");

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath).ToList();
                    var headers = lines[0];
                    var entries = lines.Skip(1).ToList();

                    var formattedTransaction = transactionToDelete
                        .Replace("Name: ", "")
                        .Replace("Type: ", "")
                        .Replace("Amount: $", "")
                        .Replace("Date: ", "")
                        .Split("\n");

                    if (formattedTransaction.Length == 4)
                    {
                        string lineToRemove = $"{formattedTransaction[0]},{formattedTransaction[2]},{formattedTransaction[1]},{formattedTransaction[3]}";

                        entries.RemoveAll(l => l.Trim() == lineToRemove.Trim());

                        File.WriteAllLines(filePath, new[] { headers }.Concat(entries));
                    }
                }

                //refresh list
                TransactionList.ItemsSource = null;
                TransactionList.ItemsSource = transactions;
            }
        }
    }

}