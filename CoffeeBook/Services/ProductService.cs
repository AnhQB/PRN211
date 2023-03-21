using CoffeeBook.DTOs;
using CoffeeBook.Enum;
using CoffeeBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
