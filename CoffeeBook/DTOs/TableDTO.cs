using CoffeeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CoffeeBook.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }

        public SolidColorBrush Background { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
    }
}
