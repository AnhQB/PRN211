using CoffeeBook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.DAOs
{
    public class TableService
    {
        private static TableService instance;
        CoffeeManagementsContext context;
/*
        public static TableService Instance
        {
            get { if (instance == null) instance = new TableService(); return TableService.instance; }
            private set { TableService.instance = value; }
        }

        public static int TableWidth = 80;
        public static int TableHeight = 80;*/

        public TableService() {
            context = new CoffeeManagementsContext();
        }

        //public void SwitchTable(int id1, int id2)
        //{
        //    DataProvider.Instance.ExecuteQuery("USP_SwitchTabel @idTable1 , @idTabel2", new object[] { id1, id2 });
        //}

        public List<Table> LoadTableList()
        {
            List<Table> tableList = context.Tables.ToList();

            return tableList;
        }

        public void UpdateTable(int id, byte status) {
            Table t = context.Tables.FirstOrDefault(x => x.Id == id);
            t.Status = status;
            context.SaveChanges();
        }
    }
}
