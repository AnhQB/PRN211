﻿using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
using CoffeeBook.Views;
using DataAccess;
using MVVMEmployee.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CoffeeBook.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
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


        TableService tableService;
        BillService billService;
        CategoryService categoryService;
        ProductService productService;

        Dictionary<int, ObservableCollection<Models.Menu>> listTableProduct;
        private ObservableCollection<TableDTO> buttonList;
        public ObservableCollection<TableDTO> ButtonList 
        {
            get { return buttonList; }
            set
            {
                buttonList = value; 
                OnPropertyChanged(nameof(ButtonList));
            }
        }
        public GeneralViewModel()
        {
            username = Settings.Username;
            listTableProduct = new Dictionary<int, ObservableCollection<Models.Menu>>();
            selectedCategory = new Category();
            selectedProduct = new Product();
            currentMenu = new Models.Menu();
            _selectedTable = new TableDTO();


            tableService = new TableService();
            billService = new BillService();
            categoryService = new CategoryService();
            productService = new ProductService();

            saveCommand = new RelayCommand(Save);
            paymentCommand = new RelayCommand(Payment);
            accountCommand = new RelayCommand(AccountInfor);
            logoutCommand = new RelayCommand(Logout);
            manageCommand = new RelayCommand(Manage);

            listCategory = new List<Category>();
            listProduct = new List<Product>();

            LoadTable();
            ButtonClickCommand = new RelayCommand<TableDTO>(ButtonClick);
            
            menuList = new ObservableCollection<Models.Menu>();
            LoadCategory();
            LoadProduct(null);
            isVisibility = true;
            if(Settings.Type != 0)
            {
                isVisibility = false;
            }

        }

        private bool isVisibility;
        public bool IsVisibility
        {
            get { return isVisibility; }
            set { isVisibility = value; OnPropertyChanged(nameof(IsVisibility)); }
        }


        private void LoadTable()
        {
            ButtonList = null;
            ButtonList = new ObservableCollection<TableDTO>(GetListTable());
        }

        private void LoadCategory()
        {
            ListCategory = null;
            ListCategory = categoryService.getListCategory(); 
        }

        public List<Category> listCategory;
        public List<Category> ListCategory
        {
            get { return listCategory; }
            set
            {
                listCategory = value; OnPropertyChanged(nameof(ListCategory));
            }
        }

        public Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory));
                LoadProduct(selectedCategory.Id);
            }
        }


        private void LoadProduct(int? cateId)
        {
            ListProduct = null;
            ListProduct = productService.getListProduct();
            if (cateId != null)
            {
                ListProduct = ListProduct.Where(x => x.IdCategory == cateId).ToList();
            }
        }

        public List<Product> listProduct;
        public List<Product> ListProduct
        {
            get { return listProduct; }
            set
            {
                listProduct = value; OnPropertyChanged(nameof(ListProduct));
                
            }
        }

        public Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public string tableName;
        public string TableName
        {
            get { return tableName; }
            set
            {
                if (tableName != value)
                {
                    tableName = value;
                    OnPropertyChanged(nameof(TableName));
                }
            }
        }

        private void ButtonClick(TableDTO parameter)
        {
            SelectedTable = parameter;
            TableName = parameter.Name;
            MenuList = new ObservableCollection<Models.Menu>();
            if (parameter.Status != 0)
            {
                
                LoadMenu(parameter.Id);
            }
        }

        private void LoadMenu(int idTable)
        {
            List<Models.Menu> menu = billService.GetListBillByTableId(idTable);
            MenuList = new ObservableCollection<Models.Menu>(menu);
            float total = 0;
            foreach(var item in MenuList)
            {
                total += item.TotalPrice;
            }
            TotalMenu = total.ToString("N0") + "đ";
        }

        private string totalMenu;
        public string TotalMenu
        {
            get { return totalMenu; }
            set
            {
                if (totalMenu != value)
                {
                    totalMenu = value;
                    OnPropertyChanged(nameof(TotalMenu));
                }
            }
        }

        private TableDTO _selectedTable;
        public TableDTO SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
            }
        }


        public ICommand ButtonClickCommand { get; set; }

        private List<TableDTO> GetListTable()
        {
            List<TableDTO> tableDTOs = new List<TableDTO>();
            foreach (var i in tableService.LoadTableList())
            {
                tableDTOs.Add(new TableDTO { 
                    Id = i.Id,
                    Name = i.Name,
                    Status = i.Status,
                    Background = i.Status == 0 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red),
                    Width = 80,
                    Height = 80
                });
            }
            return tableDTOs;
        }

        private ObservableCollection<Models.Menu> menuList;
        public ObservableCollection<Models.Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; OnPropertyChanged(nameof(MenuList)); }
        }

        private RelayCommand tableCommand;
        public RelayCommand TableCommand
        {
            get { return tableCommand; }
        }

        private Button _selectedButton;
        public Button SelectedButton
        {
            get { return _selectedButton; }
            set
            {
                _selectedButton = value;
                OnPropertyChanged(nameof(SelectedButton));
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            if(SelectedTable == null || SelectedTable.Id <= 0)
            {
                Message = "Select Table, please";
            }
            else if(selectedCategory.Id <= 0)
            {
                Message = "Select Category pls!";
            }
            else if (selectedProduct.Id <= 0)
            {
                Message = "Select Product pls!";
            }
            else if (currentMenu.Count <= 0)
            {
                Message = "Input quantity pls!";
            }
            else
            {
                Product p = productService.GetProductById(selectedProduct.Id);
                Category c = categoryService.getCategoryId(selectedCategory.Id);
                Models.Menu m = CurrentMenu;
                m.ProductName = p.Name;
                m.Price = (float)p.Price;
                m.TotalPrice = m.Count * m.Price;
                Bill i = new Bill {
                    DateCheckIn = DateTime.Now,
                    IdTable = SelectedTable.Id,
                    Status = 0,
                    CreatedBy = 1
                };
                if(MenuList.Count <= 0)
                {
                    billService.AddBill(i, p.Id, m.Count);
                }
                else
                {
                    billService.AddBillInfo(MenuList[0].Id, p.Id, m.Count);
                }

                tableService.UpdateTable(SelectedTable.Id, 1);
                LoadMenu(SelectedTable.Id);

                LoadTable();
            }
            /*try
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
            }*/
        }

        private Models.Menu currentMenu;
        public Models.Menu CurrentMenu
        {
            get { return currentMenu; }
            set
            {
                currentMenu = value;
                OnPropertyChanged(nameof(CurrentMenu));
            }
        }

        private RelayCommand paymentCommand;
        public RelayCommand PaymentCommand
        {
            get { return paymentCommand; }
        }

        public void Payment()
        {
            billService.Pay(MenuList[0].Id);
            //LoadMenu(SelectedTable.Id);
            MenuList = new ObservableCollection<Models.Menu>();
            TotalMenu = "";
            //Thread.Sleep(2000);
            LoadTable();
        }


        private RelayCommand accountCommand;
        public RelayCommand AccountCommand
        {
            get { return accountCommand; }
        }

        public void AccountInfor()
        {

            AccountProfileGUI profileGUI = new AccountProfileGUI();
            profileGUI.Show();
        }

        private RelayCommand logoutCommand;
        public RelayCommand LogoutCommand
        {
            get { return logoutCommand; }
        }

        public void Logout()
        {

            Settings.Username = "";
            Settings.Id = "";
            Settings.Type = 0;

            Login login = new Login();
            login.Show();

            Settings.CloseWin("GeneralManagementGUI");
        }



        private RelayCommand manageCommand;
        public RelayCommand ManageCommand
        {
            get { return manageCommand; }
        }

        public void Manage()
        {

            AdminGUI adminGUI = new AdminGUI();
            adminGUI.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
