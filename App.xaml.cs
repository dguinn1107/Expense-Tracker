
﻿namespace ExpenseTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var settings = new Settings();
            settings.LoadSettings();

            MainPage = new AppShell();
        }
    }
}
