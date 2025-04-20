using Microsoft.Maui.Controls;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        private ReportsViewModel reportObj;
        private SettingsModel settingsModel;
        private Expense expenseObj;


        public MainPage()
        {
            InitializeComponent();

            reportObj = new ReportsViewModel();
            DisplayChart.Chart = reportObj.LoadRandomChart();

            settingsModel = SettingsModel.LoadSettings();


            //MonthlyBudgetLabel.Text = $"Monthly Budget: {settingsModel.MonthlyBudget}";
        }

        public void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }

        public void OnSwipedLeft(object sender, EventArgs e)
        {
            DisplayChart.Chart = reportObj.LoadRandomChart();
        }

        protected override void OnAppearing() //for the "latest expense" label in MainPage.xaml
        {
            base.OnAppearing();

            var latest = Expense.MostRecentExpense(); // Reads CSV file and returns List<Expense>

            if (latest !=null)
            {
                 // validExpenses.OrderByDescending(e => e.Date).FirstOrDefault();

                LatestExpenseNameLabel.Text = latest.Name;
                LatestExpenseAmountLabel.Text = $"${latest.Amount:F2}";
                LatestExpenseCategoryLabel.Text = $"Category: {latest.Category}";
                LatestExpenseDateLabel.Text = latest.Date.ToString("MMM dd, yyyy");
            }
            else
            {
                LatestExpenseNameLabel.Text = "No expenses";
                LatestExpenseAmountLabel.Text = "";
                LatestExpenseCategoryLabel.Text = "";
                LatestExpenseDateLabel.Text = "";
            }

        }

    }
}
