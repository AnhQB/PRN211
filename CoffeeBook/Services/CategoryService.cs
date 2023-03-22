using CoffeeBook.DTOs;
using CoffeeBook.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.DAOs
{
	public class CategoryService
    {
		
		CoffeeManagementsContext context;
		public CategoryService()
		{
			context = new CoffeeManagementsContext();
		}
		public Category GetCategoryByName(string Name)
		{

			Category? category = context.Categories.Find(Name);
			return category;
		}
		public Category GetCategoryById(int Id)
		{
			Category? category = context.Categories.FirstOrDefault(x => x.Id.Equals(Id));
			return category;
		}
		public bool UpdateAccount(string Name)
		{
			bool isUpdated = false;
			try
			{
				var category = GetCategoryByName(Name);
				var NoOfRowsAffected = context.SaveChanges();
				isUpdated = NoOfRowsAffected > 0;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			//int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });
			return isUpdated;
		}
		public List<Category> getListCategory()
		{
			//return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM Account");

			List<Category> objcategoryList = new List<Category>();
			try
			{
				var ObjQuery = from obj in context.Categories
							   select obj;
				foreach (var category in ObjQuery)
				{
					objcategoryList.Add(new Category
					{
						Id = category.Id,
						Name = category.Name
						
					});
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return objcategoryList;
		}

		public bool InsertCategory(string name)
		{
			bool IsAdded = false;
			if (GetCategoryByName(name) != null)
			{
				throw new ArgumentException("Name is already exist!");
			}

			try
			{
				var category = new Category();
				category.Name = name;
				

				context.Categories.Add(category);
				var NoOfRowsAffected = context.SaveChanges();
				IsAdded = NoOfRowsAffected > 0;
			}
			catch (SqlException ex)
			{

				throw ex;
			}

			return IsAdded;
			/*string query = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type )VALUES  ( N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;*/
		}

		public bool UpdateCategory(string name)
		{
			/*string query = string.Format("UPDATE Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/

			bool isUpdated = false;
			try
			{
				var category = GetCategoryByName(name);
				
				var NoOfRowsAffected = context.SaveChanges();
				isUpdated = NoOfRowsAffected > 0;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			//int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });
			return isUpdated;
		}

		public bool DeleteCategory(string name)
		{
			/*string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/
			bool IsDeleted = false;
			try
			{
				var categoryDelete = GetCategoryByName(name);
				context.Categories.Remove(categoryDelete);
				var NoOfRowsAffected = context.SaveChanges();
				IsDeleted = NoOfRowsAffected > 0;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return IsDeleted;
		}

		

	
  }
}
