using System;
using System.Collections.Generic;

namespace CompetitionTraining2.DB;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
