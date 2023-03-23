using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
using DataAccess;
using Microsoft.IdentityModel.Abstractions;
using MVVMEmployee.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeBook.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        
        AccountService accountService;
        private RelayCommand updateCommand;
        private RelayCommand closeCommand;

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        public ProfileViewModel()
        {
            accountService = new AccountService();
            loadData(int.Parse(Settings.Id));
            updateCommand = new RelayCommand(Update);
            closeCommand = new RelayCommand(Close);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private AccountDTO account;
        public AccountDTO Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public RelayCommand CloseCommand
        {
            get { return closeCommand; }
        }

        // load view
        void loadData(int id)
        {
            Account account1 = accountService.GetAccountById(id);
            account = new AccountDTO { 
                Id = account1.Id,
                UserName = account1.UserName,
                DisplayName = account1.DisplayName,
                Type = account1.Type,
                Pass = account1.PassWord,
                TypeStr = account1.GetTypeStr()
            };
        }

        // update
        void Update()
        {
            if (account.NewPass.Equals(account.RePass))
            {

                Account a = new Account
                {
                    Id = Account.Id,
                    UserName = Account.UserName,
                    DisplayName = Account.DisplayName,
                    Type = Account.Type,
                    PassWord = Account.Pass
                };
                bool result = accountService.UpdateAccount(a, account.NewPass);
                if (result == false) MessageBox.Show("UserName is existed try another!");
                else MessageBox.Show("Update Success");
                loadData(int.Parse(Settings.Id));
            }
            else
            {
                MessageBox.Show("New Pass not equal repass");
            }
        }

        void Close()
        {
            Settings.CloseWin("AccountProfileGUI");
        }
    }
}
