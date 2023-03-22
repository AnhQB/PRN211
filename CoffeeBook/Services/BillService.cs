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

        public List<Menu> GetListBillByTableId(int tableId)
        {
            Bill bill = context.Bills.FirstOrDefault(b => b.IdTable == tableId && b.Status == 0);
            List<BillInfo> billInfos = context.BillInfos
                                        .Include(b => b.IdBillNavigation)
                                        .Include(b => b.IdProductNavigation)
                                        .Where(b => b.IdBill == bill.Id)
                                        .ToList();

            List<Menu> list = new List<Menu>();
            foreach(var item in billInfos)
            {
                list.Add(new Menu()
                {
                    Count= item.Count,
                    Price = (float)item.IdProductNavigation.Price,
                    ProductName = item.IdProductNavigation.Name,
                    TotalPrice = item.Count * (float)item.IdProductNavigation.Price
                });
            }

            
            return list;

        }

        public void AddBill(Bill i, int id, int count)
        {
            
            context.Bills.Add(i);

        }
    }
}
