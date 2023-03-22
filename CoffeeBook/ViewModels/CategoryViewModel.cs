using CoffeeBook.DAOs;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
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
    public class CategoryViewModel : INotifyPropertyChanged
    {
		CategoryService categoryService;
		public CategoryViewModel()
		{
			categoryService = new CategoryService();
			LoadData();
			CategoryAcount = new Category();
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
			set { categoryList = value; OnPropertyChanged(); }
		}
		private void LoadData()
		{
			CategoryList = null;
			CategoryList = new ObservableCollection<Category>(categoryService.getListCategory());
		}
		#endregion

		private Category currentCategory;
		public Category CategoryAcount
		{
			get { return currentCategory; }
			set { currentCategory = value; OnPropertyChanged(); }
		}

      

    }
}
