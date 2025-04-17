
﻿namespace ExpenseTracker
{
  public partial class App : Application
    {
        public static SettingsViewModel SharedSettingsViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

            SharedSettingsViewModel = new SettingsViewModel();


            MainPage = new AppShell();
        }
    }

}
