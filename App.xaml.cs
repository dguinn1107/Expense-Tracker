
﻿namespace ExpenseTracker
{
    public partial class App : Application
    {
        private SettingsViewModel _viewModel;

        public App()
        {
            InitializeComponent();

            _viewModel = new SettingsViewModel();

            _viewModel.ApplySettings();

            MainPage = new MainPage();
        }

   

    }


}
