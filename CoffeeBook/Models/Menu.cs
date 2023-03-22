using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.Models
{
  public class Menu
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; } 

        
    }
}
