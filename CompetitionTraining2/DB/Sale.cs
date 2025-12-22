using System;
using System.Collections.Generic;

namespace CompetitionTraining2.DB;

public partial class Sale
{
    public string Id { get; set; } = null!;

    public string VendingMachineId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public short Quantity { get; set; }

    public double TotalCost { get; set; }

    public DateTime Timestamp { get; set; }

    public int PaymentMethodId { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
