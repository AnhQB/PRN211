using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }
    }
}
