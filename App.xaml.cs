
﻿namespace ExpenseTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var settingsModel = SettingsModel.LoadSettings();


            MainPage = new AppShell();
        }
    }
}
