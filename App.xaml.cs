namespace ExpenseTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            var Settings = new Settings();
            Settings.LoadSettings();

            MainPage = new AppShell();
        }
    }
}
