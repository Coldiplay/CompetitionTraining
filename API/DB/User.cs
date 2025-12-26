using System.Text.Json.Serialization;

namespace API.DB;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronimyc { get; set; }

    [JsonIgnore]
    public string Last_name_fn_p => $"{LastName} {FirstName[0]}. {Patronimyc?[0]}.";
    [JsonIgnore]
    public string FIO => Patronimyc is null ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {Patronimyc}";

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public string? Phone { get; set; }

    public bool IsOperator { get; set; }

    public bool IsEngineer { get; set; }

    public bool IsManager { get; set; }

    public string? Image { get; set; }

    public string Password { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual Role Role { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachineEngineers { get; set; } = new List<VendingMachine>();
    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachineManagers { get; set; } = new List<VendingMachine>();
    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachineTechnicians { get; set; } = new List<VendingMachine>();
    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachineUsers { get; set; } = new List<VendingMachine>();
}
