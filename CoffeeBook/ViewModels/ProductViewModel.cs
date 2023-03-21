using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Enum;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoffeeBook.ViewModels
{
    public class ProductViewModel
    {
        ProductService productService;
        public ProductViewModel()
        {
            productService = new ProductService();
            productsList = new ObservableCollection<Product>();
            LoadData();
        }

        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private ObservableCollection<Product> productsList;
        public ObservableCollection<Product> ProductsList
        {
            get { return productsList; }
            set { productsList = value; OnPropertyChanged(nameof(ProductsList)); }
        }

        private void LoadData()
        {
            //AccountsList = null;
            //AccountsList = new ObservableCollection<AccountDTO>(accountService.getListAccount());
            ProductsList.Clear();
            foreach (var account in productService.getListProduct())
            {
                ProductsList.Add(account);
            }
        }

    }
}
