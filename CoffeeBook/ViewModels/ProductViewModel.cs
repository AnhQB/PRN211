﻿using AutoMapper;
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
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoffeeBook.ViewModels
{
    public class ProductViewModel
    {
        ProductService productService;

        CategoryService categoryService;

        private RelayCommand saveProductCommand;
        public RelayCommand SaveProductCommand
        {
            get { return saveProductCommand; }
        }

        private RelayCommand searchProductCommand;
        public RelayCommand SearchProductCommand
        {
            get { return searchProductCommand; }
        }

        private RelayCommand updateProductCommand;
        public RelayCommand UpdateProductCommand
        {
            get { return updateProductCommand; }
        }

        private RelayCommand<int> deleteProductCommand;
        public ICommand DeleteProductCommand
        {
            get { return deleteProductCommand; }
        }

        private string searchProductName;

        public string SearchProductName
        {
            get { return searchProductName; }
            set { searchProductName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> productsList;
        public ObservableCollection<Product> ProductsList
        {
            get { return productsList; }
            set { productsList = value; OnPropertyChanged(nameof(ProductsList)); }
        }

        public ProductViewModel()
        {
            categoryService = new CategoryService();
            productService = new ProductService();
            productsList = new ObservableCollection<Product>();
            saveProductCommand = new RelayCommand(Save);
            searchProductCommand = new RelayCommand(Search);
            updateProductCommand = new RelayCommand(Update);
            CurrentProduct = new Product();
            deleteProductCommand = new RelayCommand<int>(Delete);
            GetCategories();
            LoadData();
        }

        public ObservableCollection<Category> getCategoriesB { get; set; } = new ObservableCollection<Category>();

        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Product currentProduct;
        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                currentProduct = value;
                OnPropertyChanged();
            }
        }

        #endregion    

        private void LoadData()
        {
            ProductsList.Clear();
            foreach (var product in productService.getListProduct())
            {
                ProductsList.Add(product);
            }
        }

        private void Save()
        {
            String Message;
            try
            {
                currentProduct.IdCategory = productService.GetCategoryID(currentProduct.IdCategory);
                currentProduct.IdCategoryNavigation = categoryService.GetCategoryById(currentProduct.IdCategory);
                var IsSaved = productService.InsertData(currentProduct);              
                LoadData();
                if (IsSaved)
                    Message = "Product saved";
                else
                    Message = "Add failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void GetCategories()
        {       
            List<Category> categories = categoryService.getListCategory();
            foreach (var category in categories)
            {
                getCategoriesB.Add(category);
            }
        }

        private void Update()
        {
            String Message;
            try
            {                  
                    var IsSaved = productService.UpdateData(currentProduct);
                    LoadData();
                    if (IsSaved)
                        Message = "Product saved";
                    else
                        Message = "Update failed";

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void Search()
        {
            ProductsList.Clear();
            foreach (var product in productService.getListProduct())
            {
                if (product.Name.Contains(SearchProductName))
                {
                    ProductsList.Add(product);
                }            
            }
        }

        private void Delete(int productId)
        {
            String Message;
            try
            {
                Product product = productService.GetProductById(productId);
                var IsSaved = productService.DeleteProduct(product);
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
    }
}
