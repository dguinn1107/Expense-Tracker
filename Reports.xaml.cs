namespace ExpenseTracker;

public partial class Reports : ContentPage
{
    public Reports()
    {
        InitializeComponent();
        BindingContext = new ReportsViewModel();


    }

}
