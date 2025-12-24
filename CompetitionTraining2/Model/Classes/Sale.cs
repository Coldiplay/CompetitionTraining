using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model.Classes;

public partial class Sale
{
    public int Id { get; set; }

    public string VendingMachineId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public short Quantity { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime Timestamp { get; set; }

    public int PaymentMethodId { get; set; }

    //[JsonIgnore]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    //[JsonIgnore]
    public virtual Product Product { get; set; } = null!;
    //[JsonIgnore]
    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
