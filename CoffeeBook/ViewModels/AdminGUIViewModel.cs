﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.ViewModels
{
    public class AdminGUIViewModel
    {

        public AccountViewModel AccountViewModel { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }

        public ProductViewModel ProductViewModel { get; set; }

        public TurnOverViewModel TurnOverViewModel { get; set; }
    }
}
