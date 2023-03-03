using System;
using System.Collections.Generic;

namespace CoffeeBook.Models;

public partial class Account
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string PassWord { get; set; }

    public byte Type { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
