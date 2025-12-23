using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model.Classes;

public partial class PaymentType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
