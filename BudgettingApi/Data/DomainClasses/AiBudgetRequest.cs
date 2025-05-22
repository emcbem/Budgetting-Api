using System.Text.Json.Serialization;
using GenerativeAI.Types;

namespace BudgettingApi.Data;

public class AiBudget{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("description")]
    public string BudgetDescription { get; set; } = "";
    [JsonPropertyName("incomePercentage")]
    public float IncomePercentage {get; set;}
}

public class AiBudgetRequest{
    [JsonPropertyName("overview")]
    
    public string Overview {get; set;} = "";

    [JsonPropertyName("aiBudgets")]
    public List<AiBudget> AiBudgets {get; set;} = [];
}