using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.DB;

public partial class StatusVendingMachine
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
