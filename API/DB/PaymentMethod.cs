using System.Text.Json.Serialization;

namespace API.DB;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
