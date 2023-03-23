using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccess
{
    public class Settings
    {
        public static string Id = "";
        public static string Username = "";
        public static byte Type = 0;

        public static void CloseWin(string nameGUI)
        {
            WindowCollection a = Application.Current.Windows;
            foreach (Window item in a)
            {
                if (item.Title.Equals(nameGUI))
                {
                    item.Hide();
                }
            }
        }
    }
}
