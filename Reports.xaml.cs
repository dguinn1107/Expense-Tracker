namespace ExpenseTracker;

public partial class Reports : ContentPage
{
    public Reports()
    {
        InitializeComponent();
        BindingContext = new ReportsViewModel();


    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {

    }
}
