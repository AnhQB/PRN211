using CoffeeBook.DAOs;
using CoffeeBook.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoffeeBook.ViewModels
{
    public class CategoryViewModel
    {
        CategoryService categoryService;
        public CategoryViewModel()
        {
            categoryService = new CategoryService();
            CategoryList = new ObservableCollection<Category>();
            LoadData();
            CurrentCategory = new Category();
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
            foreach (var product in categoryService.getListCategory())
            {
                CategoryList.Add(product);
            }
        }
        #endregion

        private Category currentCategory;
        public Category CurrentCategory
        {
            get { return currentCategory; }
            set { currentCategory = value; OnPropertyChanged(); }
        }



    }
}
