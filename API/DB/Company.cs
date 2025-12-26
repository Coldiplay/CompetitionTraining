using System.Text.Json.Serialization;

namespace API.DB;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
