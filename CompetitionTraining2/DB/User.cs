using System.Text.Json.Serialization;

namespace CompetitionTraining2.DB;

public partial class User
{
    public string Id { get; set; } = null!;
    public string Fio { get; set; } = null!;
    public string? Email { get; set; }

    public int RoleId { get; set; }
    public string? Phone { get; set; }
    public bool IsOperator { get; set; }
    public bool IsEngineer { get; set; }
    public bool IsManager { get; set; }
    public string? Image { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachineEngineers { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineManagers { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineOperators { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineTechnicians { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineUsers { get; set; } = new List<VendingMachine>();
}
