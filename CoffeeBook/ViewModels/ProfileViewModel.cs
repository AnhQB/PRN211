using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
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
    public class ProfileViewModel
    {
        private Account account;
        AccountService accountService;
        private RelayCommand updateCommand;
        private RelayCommand closeCommand;

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        public ProfileViewModel(int id)
        {
            accountService = new AccountService();
            loadData(id);
            updateCommand = new RelayCommand(Update);
            closeCommand = new RelayCommand(Close);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Account Account
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
            account = accountService.GetAccountById(id);
        }

        // update
        void Update()
        {
            var a = accountService.UpdateAccount(account);
            if (!a) MessageBox.Show("UserName is existed try another!");
            else MessageBox.Show("Update Success");
        }

        void Close()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
