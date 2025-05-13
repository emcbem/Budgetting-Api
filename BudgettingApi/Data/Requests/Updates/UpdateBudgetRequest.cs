using System.Text.Json.Serialization;

namespace BudgettingApi.Data.Requests.Updates;

public class UpdateBudgetRequest {
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("percentage")]
    public float BudgetPercentage { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

}