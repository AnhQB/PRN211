using System;
using System.Collections.Generic;

namespace CoffeeBook.Models;

public partial class Table
{
    public int Id { get; set; }

    public string Name { get; set; }

    public byte Status { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
