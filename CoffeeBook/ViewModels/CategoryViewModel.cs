using CoffeeBook.DAOs;
using CoffeeBook.Models;
using MaterialDesignColors;
using MVVMEmployee.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoffeeBook.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
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

        CategoryService categoryService;

        private Category currentCategory;
        public Category CurrentCategory
        {
            get { return currentCategory; }
            set { currentCategory = value; OnPropertyChanged(); }
        }

        private RelayCommand saveCategoryCommand;
        public RelayCommand SaveCategoryCommand
        {
            get { return saveCategoryCommand; }
        }

        private RelayCommand searchCategoryCommand;
        public RelayCommand SearchCategoryCommand
        {
            get { return searchCategoryCommand; }
        }

        private RelayCommand updateCategoryCommand;
        public RelayCommand UpdateCategoryCommand
        {
            get { return updateCategoryCommand; }
        }

        private RelayCommand<int> deleteCategoryCommand;
        public ICommand DeleteCategoryCommand
        {
            get { return deleteCategoryCommand; }
        }


        private string sarchCategorytName;

        public string SearchCategorytName
        {
            get { return sarchCategorytName; }
            set { sarchCategorytName = value; OnPropertyChanged(); }
        }

        public CategoryViewModel()
        {          
            categoryService = new CategoryService();
            categoryList = new ObservableCollection<Category>();            
            saveCategoryCommand = new RelayCommand(Save);
            searchCategoryCommand = new RelayCommand(Search);
            updateCategoryCommand = new RelayCommand(Update);
            CurrentCategory = new Category();
            deleteCategoryCommand = new RelayCommand<int>(delete);        
            LoadData();          
        }

        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region DisplayOperation
        private ObservableCollection<Category> categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged(nameof(CategoryList)); }
        }
        private void LoadData()
        {
            CategoryList.Clear();
            foreach (var category in categoryService.getListCategory())
            {
                CategoryList.Add(category);
            }
        }
        #endregion

        private void Save()
        {
            try
            {
                if (currentCategory.Name != "" && currentCategory.Id > 0)
                {
                    var IsSaved = categoryService.InsertCategory(currentCategory.Name);
                    LoadData();
                    if (IsSaved)
                        Message = "Category saved";
                    else
                        Message = "Add failed";
                }
                else Message = "Input ple";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }


        private void Search()
        {
            CategoryList.Clear();
            String s;
            if (SearchCategorytName == null) s = "";
            else s = SearchCategorytName;
            foreach (var category in categoryService.getListCategory())
            {
                if (category.Name.Contains(s))
                {
                    CategoryList.Add(category);
                }
            }
        }

        private void delete(int categoryId)
        {
            try
            {
                
                Category category = categoryService.GetCategoryById(categoryId);
                var IsSaved = categoryService.DeleteCategory(category.Name);
                LoadData();
                if (IsSaved)
                    Message = "Delete successful";
                else
                    Message = "Delete failed";
             
                    
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void Update()
        {
            try
            {
                if (currentCategory.Name != "" && currentCategory.Id > 0 )
                {
                    var IsSaved = categoryService.UpdateCategory(currentCategory);
                    LoadData();
                    if (IsSaved)
                        Message = "Product saved";
                    else
                        Message = "Update failed";
                }
                else
                {
                    Message = "Select And Input Please";
                }
                    

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }      
    }
}
