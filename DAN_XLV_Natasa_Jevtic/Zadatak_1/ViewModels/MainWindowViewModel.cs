using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow mainWindow;

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(LogInExecute, CanLogInExecute);
                }
                return logIn;
            }
        }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// This method checks if username and password valid.
        /// </summary>
        /// <param name="password">User input for password.</param>
        public void LogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (Username == "Man2019" && Password == "Man2019")
            {
                ManagerView managerView = new ManagerView();
                managerView.ShowDialog();
            }
            else if (Username == "Mag2019" && Password == "Mag2019")
            {
                WarehouseClerkView warehouseClerkView = new WarehouseClerkView();
                warehouseClerkView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong username or password. Please, try again.", "Notification");
            }
        }
        /// <summary>
        /// This method ensures that the login can only be executed when the login fields are not empty.
        /// </summary>
        /// <param name="password">User input for password.</param>
        /// <returns>True if login can execute, false if not.</returns>
        public bool CanLogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}