using CoffeeBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.ViewModels
{
    public class BillInfor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BillInfor(List<BillInfo> list)
        {
            double total = 0;
            foreach(BillInfo i in list)
            {
                total += i.IdProductNavigation.Price;
            }
            billInforList = list;
            idBill = "Id Bill: " + billInforList[0].IdBill;
            totalPrice = "Total: " + total;
        }

        private List<BillInfo> billInforList;
        public List<BillInfo> BillInforList
        {
            get { return billInforList; }
            set
            {
                billInforList = value;
                OnPropertyChanged(nameof(BillInforList));
            }
        }
        private string totalPrice;
        public string TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private string idBill;
        public string IdBill
        {
            get { return idBill; }
            set
            {
                idBill = value;
                OnPropertyChanged(nameof(IdBill));
            }
        }
    }
}
