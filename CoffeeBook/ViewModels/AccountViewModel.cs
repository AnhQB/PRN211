using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.ViewModels
{
    public class AccountViewModel
    {
        AccountService accountService;
        public AccountViewModel()
        {
            accountService = new AccountService();
            LoadData();
            CurrentAccount = new AccountDTO();
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


    }
}
