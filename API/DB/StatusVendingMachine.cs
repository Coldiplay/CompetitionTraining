using System;
using System.Collections.Generic;

namespace API.DB;

public partial class StatusVendingMachine
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
