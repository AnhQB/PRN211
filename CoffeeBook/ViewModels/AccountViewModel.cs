using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
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
using System.Windows.Input;

namespace CoffeeBook.ViewModels
{
    public class AccountViewModel
    {
        AccountService accountService;

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }
        public AccountViewModel()
        {
            accountService = new AccountService();
            LoadData();
            CurrentAccount = new AccountDTO();
            saveCommand = new RelayCommand(Save);
        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region DisplayOperation
        private ObservableCollection<AccountDTO> accountsList;
        public ObservableCollection<AccountDTO> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; OnPropertyChanged(); }
        }
        private void LoadData()
        {
            AccountsList = new ObservableCollection<AccountDTO>(accountService.getListAccount());
        }
        #endregion

        private AccountDTO currentAccount;
        public AccountDTO CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; OnPropertyChanged(); }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var a = currentAccount;
                /*var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";*/
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
