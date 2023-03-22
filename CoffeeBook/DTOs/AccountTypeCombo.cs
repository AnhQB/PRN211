using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.DTOs
{
    public class AccountTypeCombo
    {
        public string Display { get; set; }
        public string Value { get; set; }

        public AccountTypeCombo() { }

        public AccountTypeCombo(string display, string value)
        {
            Display = display;
            Value = value;
        }
    }
}
