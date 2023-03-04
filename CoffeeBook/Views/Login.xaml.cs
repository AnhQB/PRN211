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
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;
using CoffeeBook.Models;
using CoffeeBook.ViewModels;

namespace CoffeeBook.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        public Login()
        {
            InitializeComponent();
			
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
           
		}
       
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
		}

	}
}
