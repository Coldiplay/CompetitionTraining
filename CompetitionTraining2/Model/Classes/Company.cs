using API.DB;
using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model.Classes;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
