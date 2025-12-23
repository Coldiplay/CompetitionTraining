using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model.Classes;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
