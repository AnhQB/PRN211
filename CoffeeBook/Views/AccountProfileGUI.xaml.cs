using CoffeeBook.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoffeeBook.Views
{
    /// <summary>
    /// Interaction logic for AccountProfileGUI.xaml
    /// </summary>
    public partial class AccountProfileGUI : Window
    {
        AccountService accountService = new AccountService();
        public AccountProfileGUI()
        {
            int id = 1;
            InitializeComponent();
            this.DataContext = accountService.GetAccountById(id);
        }
    }
}
