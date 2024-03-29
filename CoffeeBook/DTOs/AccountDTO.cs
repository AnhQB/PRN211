﻿using System;
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

        public byte Type { get; set; }

        public string TypeStr { get; set; }

        public string Pass { get; set; }

        public string NewPass { get; set; }
        public string RePass { get; set; }
    }
}
