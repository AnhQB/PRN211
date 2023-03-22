using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoffeeBook.Services
{
    public class ValueConverters: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)  
        {  
            if (int.Parse(value.ToString()) == 0)  
            {  
                return null;  
            }  
            else  
            {  
                return value;  
            }  
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
