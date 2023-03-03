using System;
using System.Collections.Generic;

namespace CoffeeBook.Models;

public partial class Bill
{
    public int Id { get; set; }

    public DateTime DateCheckIn { get; set; }

    public DateTime? DateCheckOut { get; set; }

    public int IdTable { get; set; }

    public int Status { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; } = new List<BillInfo>();

    public virtual Account CreatedByNavigation { get; set; }

    public virtual Table IdTableNavigation { get; set; }
}
