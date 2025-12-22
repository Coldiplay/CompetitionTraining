using System.Text.Json.Serialization;

namespace CompetitionTraining2.Model
{
    class UserImport
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("full_name")]
        public string Fio { get; set; } = null!;

        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("is_operator")]
        public bool IsOperator { get; set; }
        [JsonPropertyName("is_engineer")]
        public bool IsEngineer { get; set; }
        [JsonPropertyName("is_manager")]
        public bool IsManager { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
