using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
using CoffeeBook.Views;
using DataAccess;
using MVVMEmployee.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CoffeeBook.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        AccountService accountService;
        public LoginViewModel() 
        {
            accountService = new AccountService();
            loginCommand = new RelayCommand(Login);
            exitCommand = new RelayCommand(Exit);
            currentAccount = new Account();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SecureString _password;

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public Account currentAccount;
        public Account CurrentAccount
        {
            get { return currentAccount; }
            set
            {
                currentAccount = value; OnPropertyChanged(nameof(CurrentAccount));
            }
        }


        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get { return loginCommand; }
        }

        public void Login()
        {
            if(accountService.Login(currentAccount.UserName, currentAccount.PassWord) == false)
            {
                MessageBox.Show("Login fail", "Warning");
            }
            else
            {
                Account a = accountService.GetAccountByUserName(currentAccount.UserName);
                Settings.Username = a.UserName;
                Settings.Id = a.Id + "";
                Settings.Type = a.Type;
                //Application.Current.MainWindow.Close();
                Login l = new Login();
                
                GeneralManagementGUI genGUI = new GeneralManagementGUI();
                genGUI.Show();
                

            }

        }

        private RelayCommand exitCommand;
        public RelayCommand ExitCommand
        {
            get { return exitCommand; }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
