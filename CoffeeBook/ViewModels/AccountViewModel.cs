using AutoMapper;
using CoffeeBook.Configuration;
using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Enum;
using CoffeeBook.Models;
using MVVMEmployee.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoffeeBook.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        AccountService accountService;

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
                MessageBox.Show(message, "Warning");
            }
        }
        public AccountViewModel()
        {
            accountService = new AccountService();
            AccountsList = new ObservableCollection<AccountDTO>();
            //AccountTypeEnumValues = new ObservableCollection<AccountTypeEnum>
            //(System.Enum.GetValues(typeof(AccountTypeEnum))
            //.Cast<AccountTypeEnum>());
            AccountTypeCombos = new List<AccountTypeCombo>()
            {
                new AccountTypeCombo("Admin", "0"),
                new AccountTypeCombo("Staff", "1"),
            };
            SelectedAccountType = AccountTypeCombos[1];

            LoadData();
            currentAccount = new AccountDTO();
            //CurrentAccount = new AccountDTO();
            saveCommand = new RelayCommand(Save);
            deleteCommand = new RelayCommand<int>(Delete);
            updateCommand = new RelayCommand(Update);
            selectedCommand = new RelayCommand(lvAccount_SelectionChanged);
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
            set { accountsList = value; OnPropertyChanged(nameof(AccountsList)); }
        }

        public ObservableCollection<AccountTypeEnum> AccountTypeEnumValues { get; set; } = new ObservableCollection<AccountTypeEnum>();

        private AccountTypeEnum _selectedAccountTypeEnum = AccountTypeEnum.Staff;
        public AccountTypeEnum SelectedAccountTypeEnum
        {
            get { return _selectedAccountTypeEnum; }
            set { _selectedAccountTypeEnum = value; OnPropertyChanged(); }
        }

        public List<AccountTypeCombo> accountTypeCombos;
        public List<AccountTypeCombo> AccountTypeCombos
        {
            get { return accountTypeCombos; }
            set
            {
                accountTypeCombos = value; OnPropertyChanged(nameof(AccountTypeCombos));
            }
        }


        public AccountTypeCombo selectedAccountType;
        public AccountTypeCombo SelectedAccountType
        {
            get { return selectedAccountType; }
            set
            {
                selectedAccountType = value; OnPropertyChanged(nameof(SelectedAccountType));
            }
        }

        private void LoadData()
        {
            //AccountsList = null;
            //AccountsList = new ObservableCollection<AccountDTO>(accountService.getListAccount());
            AccountsList.Clear();
            foreach (var account in accountService.getListAccount())
            {
                AccountsList.Add(account);
            }
        }
        #endregion

        private AccountDTO currentAccount;
        public AccountDTO CurrentAccount
        {
            get { return currentAccount; }
            set {
                currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
                if(currentAccount != null) {
                    SelectedAccountType = AccountTypeCombos.First(x => x.Value == currentAccount.Type.ToString());
                    OnPropertyChanged(nameof(SelectedAccountType));
                }
            }
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
                if(currentAccount.UserName != "")
                {
                    currentAccount.Type = (byte)SelectedAccountTypeEnum;
                    var IsSaved = accountService.InsertAccount(currentAccount);
                    LoadData();
                    if (IsSaved)
                        Message = "Account saved";
                    else
                        Message = "Save account failed";
                }
                else
                {
                    Message = "Select Account Please";
                }
                
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private RelayCommand<int> deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        private void Delete(int accountId)
        {
            try
            {
                Account user = accountService.GetAccountById(accountId);

                var IsSaved = accountService.DeleteAccount(user);
                LoadData();
                if (IsSaved)
                    Message = "Account deleted";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                if(currentAccount.Id > 0)
                {
                    currentAccount.Type = (byte)SelectedAccountTypeEnum;
                    Account user = accountMapper(currentAccount);
                    var IsSaved = accountService.UpdateAccount(user, currentAccount.NewPass);
                    LoadData();
                    if (IsSaved)
                        Message = "Account saved";
                    else
                        Message = "Update operation failed";
                }
                else
                {
                    Message = "Select Account Pls";
                }
                
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private Account accountMapper(AccountDTO account)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            Account user = mapper.Map<Account>(account);
            return user;
        }

        private RelayCommand selectedCommand;
        public RelayCommand SelectedCommand
        {
            get { return selectedCommand; }
        }

        private void lvAccount_SelectionChanged()
        {
            if (currentAccount != null)
            {
                var account = currentAccount;
                if (account.Type == (byte)AccountTypeEnum.Staff)
                {
                    SelectedAccountTypeEnum = AccountTypeEnum.Staff;
                }
                else if (account.Type == (byte)AccountTypeEnum.Admin)
                {
                    SelectedAccountTypeEnum = AccountTypeEnum.Admin;
                }
            }
        }

        private string searchAccount;
        public string SearchAccount
        {
            get { return searchAccount; }
            set
            {
                searchAccount = value;
                OnPropertyChanged(nameof(SearchAccount));
                AccountsList = new ObservableCollection<AccountDTO>(accountService.SearchAccountByUserName(searchAccount));
            }
        }
    }
}
