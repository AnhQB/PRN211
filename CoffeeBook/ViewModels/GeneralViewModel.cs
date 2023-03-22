using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CoffeeBook.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
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
            listTableProduct = new Dictionary<int, ObservableCollection<Models.Menu>>();
            tableService = new TableService();
            billService = new BillService();
            categoryService = new CategoryService();
            productService = new ProductService();

            /*ButtonList = new ObservableCollection<Button>()
            {
                new Button { Content = "Button 1" },
                new Button { Content = "Button 2" },
                new Button { Content = "Button 3" },
                new Button { Content = "Button 4" },
                new Button { Content = "Button 5" },
                new Button { Content = "Button 6" },

            };*/
            saveCommand = new RelayCommand(Save);
            listCategory = new List<Category>();
            listProduct = new List<Product>();
            LoadTable();
            ButtonClickCommand = new RelayCommand<TableDTO>(ButtonClick);
            currentMenu = new Models.Menu();
            ProductsList = new ObservableCollection<Models.Menu>();
            LoadCategory();
            LoadProduct(null);
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
                ListProduct.Where(x => x.IdCategory == cateId);
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
            ProductsList = null;
            if (parameter.Status != 0)
            {
                List<Models.Menu> menu = billService.GetListBillByTableId(parameter.Id);
                ProductsList = new ObservableCollection<Models.Menu>(menu);
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

        private ObservableCollection<Models.Menu> productsList;
        public ObservableCollection<Models.Menu> ProductsList
        {
            get { return productsList; }
            set { productsList = value; OnPropertyChanged(nameof(ProductsList)); }
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


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            if(SelectedTable == null || SelectedTable.Id <= 0)
            {
                MessageBox.Show("Select Table, please", "Warning");
            }
            else
            {
                Product p = productService.getProductId(selectedProduct.Id);
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

                billService.AddBill(i, p.Id, m.Count);

                tableService.UpdateTable(SelectedTable.Id, 1);
                
                ProductsList.Add(m);

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








        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


}
