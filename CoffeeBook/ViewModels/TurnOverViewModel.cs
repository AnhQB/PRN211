using CoffeeBook.DAOs;
using CoffeeBook.Models;
using CoffeeBook.Views;
using MVVMEmployee.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeeBook.ViewModels
{
    public class TurnOverViewModel : INotifyPropertyChanged
    {
        BillService billService;
        public TurnOverViewModel()
        {
            searchCommand = new RelayCommand(Search);
            detailCommand = new RelayCommand<int>(Detail);

            billService = new BillService();
            billList = new ObservableCollection<Bill>();
            string date = DateTime.Now.ToString();
            fromDate = DateTime.Now + "";
            toDate = DateTime.Now + "";
            LoadData(date, date);

        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string fromDate;
        public string FromDate
        {
            get { return fromDate; }
            set { fromDate = value; OnPropertyChanged(nameof(FromDate)); }
        }


        private string toDate;
        public string ToDate
        {
            get { return toDate; }
            set { toDate = value; OnPropertyChanged(nameof(ToDate)); }
        }

        private ObservableCollection<Bill> billList;
        public ObservableCollection<Bill> BillList
        {
            get { return billList; }
            set { billList = value; OnPropertyChanged(); }
        }
        private void LoadData(string fromDate, string toDate)
        {
            BillList = new ObservableCollection<Bill>();
            foreach (var account in billService.getListBill(fromDate, toDate))
            {
                BillList.Add(account);
            }
        }


        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            LoadData(FromDate, ToDate);
        }


        private RelayCommand<int> detailCommand;
        public ICommand DetailCommand
        {
            get { return detailCommand; }
        }

        public void Detail(int idBill)
        {
            List<BillInfo> billInfos = billService.GetBillInfos(idBill);
            BillInforGUI billInforGUI = new BillInforGUI(billInfos);
            billInforGUI.Show();
        }
    }
}
