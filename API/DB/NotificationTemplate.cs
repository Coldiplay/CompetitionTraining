using System;
using System.Collections.Generic;

namespace API.DB;

public partial class NotificationTemplate
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
