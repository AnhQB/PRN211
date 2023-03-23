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

        public List<Bill> getListBill(string fromDate, string toDate)
        {
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate);
            List<Bill> objAccountList = new List<Bill>();
            try
            {
                objAccountList = context.Bills
                    .Include(b => b.IdTableNavigation)
                    .Include(b => b.CreatedByNavigation)
                    .Where(b => b.DateCheckIn >= from && b.DateCheckIn <= to)
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
                    Id = item.IdBill,
                    Count= item.Count,
                    Price = (float)item.IdProductNavigation.Price,
                    ProductName = item.IdProductNavigation.Name,
                    TotalPrice = item.Count * (float)item.IdProductNavigation.Price
                });
            }

            
            return list;

        }

        public void AddBill(Bill i, int proId, int count)
        {
            
            context.Bills.Add(i);
            context.SaveChanges();

            context.BillInfos.Add(new BillInfo { Count = count, IdBill = i.Id, IdProduct = proId });
            context.SaveChanges();
        }

        internal void AddBillInfo(int idBill, int proId, int count)
        {
            BillInfo billInfo = context.BillInfos.FirstOrDefault(x => x.IdProduct == proId && x.IdBill == idBill);
            if (billInfo == null)
            {
                context.BillInfos.Add(new BillInfo { Count = count, IdBill = idBill, IdProduct = proId });
            }
            else
            {
                billInfo.Count += count;
                context.BillInfos.Update(billInfo);
            }
            context.SaveChanges();
        }

        internal void Pay(int id)
        {
            Bill bill = context.Bills.FirstOrDefault(x => x.Id == id);
            if(bill != null)
            {
                bill.Status = 1;
                bill.DateCheckOut = DateTime.Now;
                context.Update(bill);

                Table t = context.Tables.FirstOrDefault(x => x.Id == bill.IdTable);
                t.Status = 0;
                context.Update(t);
                context.SaveChanges();
            }
        }

        public List<BillInfo> GetBillInfos(int idBill)
        {
            List<BillInfo> list = context.BillInfos
                .Include(x => x.IdBillNavigation)
                .Include(x => x.IdProductNavigation)
                .Where(x => x.IdBill == idBill)
                .ToList();
            return list;
        }
    }
}
