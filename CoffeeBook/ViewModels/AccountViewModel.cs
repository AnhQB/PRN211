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
            AccountTypeEnumValues = new ObservableCollection<AccountTypeEnum>(System.Enum.GetValues(typeof(AccountTypeEnum)).Cast<AccountTypeEnum>());

            LoadData();
            CurrentAccount = new AccountDTO();
            saveCommand = new RelayCommand(Save);
            deleteCommand = new RelayCommand<AccountDTO>(Delete);
            updateCommand = new RelayCommand(Update);
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

        private AccountTypeEnum _selectedEnum;
        public AccountTypeEnum SelectedEnum
        {
            get { return _selectedEnum; }
            set { _selectedEnum = value; OnPropertyChanged(); }
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
                currentAccount.Type = (byte)_selectedEnum;
                var IsSaved = accountService.InsertAccount(currentAccount);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private RelayCommand<AccountDTO> deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        private void Delete(AccountDTO account)
        {
            try
            {
               
                Account user = accountMapper(account);

                var IsSaved = accountService.DeleteAccount(user);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
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
                currentAccount.Type = (byte)_selectedEnum;
                Account user = accountMapper(currentAccount);
                var IsSaved = accountService.UpdateAccount(user, currentAccount.NewPass);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
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
    }
}
