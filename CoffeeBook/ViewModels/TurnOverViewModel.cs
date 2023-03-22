using CoffeeBook.DAOs;
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
    public class TurnOverViewModel : INotifyPropertyChanged
    {
        BillService billService;
        public TurnOverViewModel()
        {
            billService = new BillService();
            billList = new ObservableCollection<Bill>();
            LoadData();
        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private ObservableCollection<Bill> billList;
        public ObservableCollection<Bill> BillList
        {
            get { return billList; }
            set { billList = value; OnPropertyChanged(); }
        }
        private void LoadData()
        {
            BillList = null;
            foreach (var account in billService.getListBill())
            {
                BillList.Add(account);
            }
        }
    }
}
