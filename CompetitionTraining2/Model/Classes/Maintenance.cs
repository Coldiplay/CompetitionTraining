using API.DB;
using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model.Classes;

public partial class Maintenance
{
    public int Id { get; set; }

    public string? IssuesFound { get; set; }

    public string VendingMachineId { get; set; } = null!;

    public string TechnicianGuid { get; set; } = null!;

    public string WorkDescription { get; set; } = null!;

    public DateTime Date { get; set; }

    [JsonIgnore]
    public virtual User Technician { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
