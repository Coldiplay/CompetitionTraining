using System;
using System.Collections.Generic;

namespace API.DB;

public partial class Product : IBaseModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public short MinStock { get; set; }

    public string VendingMachineId { get; set; } = null!;

    public string? Description { get; set; }

    public ushort QuantityAvailable { get; set; }

    public double SalesTrend { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
