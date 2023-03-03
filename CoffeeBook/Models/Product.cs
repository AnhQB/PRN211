using System;
using System.Collections.Generic;

namespace CoffeeBook.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int IdCategory { get; set; }

    public double Price { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; } = new List<BillInfo>();

    public virtual Category IdCategoryNavigation { get; set; }
}
