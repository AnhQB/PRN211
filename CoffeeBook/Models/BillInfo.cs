using System;
using System.Collections.Generic;

namespace CoffeeBook.Models;

public partial class BillInfo
{
    public int Id { get; set; }

    public int IdBill { get; set; }

    public int IdProduct { get; set; }

    public int Count { get; set; }

    public virtual Bill IdBillNavigation { get; set; }

    public virtual Product IdProductNavigation { get; set; }
}
