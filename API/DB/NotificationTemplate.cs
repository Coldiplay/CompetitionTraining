using System.Text.Json.Serialization;

namespace API.DB;

public partial class NotificationTemplate
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
