﻿using AutoMapper;
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
            AccountsList = new ObservableCollection<AccountDTO>();
            AccountTypeEnumValues = new ObservableCollection<AccountTypeEnum>(System.Enum.GetValues(typeof(AccountTypeEnum)).Cast<AccountTypeEnum>());
            LoadData();
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
                OnPropertyChanged();

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
                currentAccount.Type = (byte)SelectedAccountTypeEnum;
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
                currentAccount.Type = (byte)SelectedAccountTypeEnum;
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
    }
}
