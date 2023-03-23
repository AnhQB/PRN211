using CoffeeBook.DTOs;
using CoffeeBook.Enum;
using CoffeeBook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CoffeeBook.DAOs
{
    public class ProductService
    {

        CoffeeManagementsContext context;
        public ProductService()
        {
            context = new CoffeeManagementsContext();
        }

        public List<Product> getListProduct()
        {
            //return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM Account");

            List<Product> objAccountList = new List<Product>();
            try
            {
                objAccountList = context.Products
                    .Include(p => p.IdCategoryNavigation)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAccountList;
        }

        public bool InsertData(Product product)
        {
            bool IsAdded = false;
            if (GetProductByName(product.Name) != null)
            {
                throw new ArgumentException("Product is already exist!");
            }

            try
            {

                context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Product] ([Name] ,[IdCategory] ,[Price]) VALUES ('" + product.Name + "', " + product.IdCategory + ", " + product.Price + " );");
                IsAdded = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return IsAdded;
        }

        public Product GetProductByName(string name)
        {
            Product? product = context.Products.FirstOrDefault(u => u.Name.Equals(name));
            return product;
        }

        public int GetCategoryID(int IdCategory)
        {
            List<Category> c = context.Categories.ToList();
            for(int i=0;i<c.Count; i++)
            {
                if (c[i].Id.Equals(IdCategory))
                {
                    return c[i+1].Id;
                }
            }
            return 0;
        }

        public bool UpdateData(Product product)
        {
            bool isUpdated = false;
            try
            {
                var p1 = GetProductById(product.Id);
                if (p1 != null)
                {
                    p1.Name = product.Name;
                    p1.Price = product.Price;
                    p1.IdCategory = product.IdCategory;
                    p1.IdCategoryNavigation = product.IdCategoryNavigation;

                    var NoOfRowsAffected = context.SaveChanges();
                    isUpdated = NoOfRowsAffected > 0;
                }
                else return isUpdated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public bool DeleteProduct(Product product)
        {

            bool IsDeleted = false;
            try
            {
                var productDelete = GetProductById(product.Id);
                context.Products.Remove(productDelete);
                var NoOfRowsAffected = context.SaveChanges();
                IsDeleted = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return IsDeleted;
        }

        public Product GetProductById(int Id)
        {
            Product? product = context.Products.Find(Id);
            return product;
        }
    }
}
