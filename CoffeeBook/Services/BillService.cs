using CoffeeBook.DTOs;
using CoffeeBook.Enum;
using CoffeeBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.DAOs
{
    public class BillService
    {
        CoffeeManagementsContext context;
        public BillService()
        {
            context = new CoffeeManagementsContext();
        }

        public List<Bill> getListBill()
        {
            //return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM Account");

            List<Bill> objAccountList = new List<Bill>();
            try
            {
                objAccountList = context.Bills
                    .Include(b => b.IdTableNavigation)
                    .Include(b => b.CreatedByNavigation)
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
