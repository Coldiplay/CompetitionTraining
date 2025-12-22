using System;
using System.Collections.Generic;

namespace CompetitionTraining2.DB;

public partial class Maintenance
{
    public int Id { get; set; }

    public string? IssuesFound { get; set; }

    public string VendingMachineId { get; set; } = null!;

    public string TechnicianGuid { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual User Technician { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
