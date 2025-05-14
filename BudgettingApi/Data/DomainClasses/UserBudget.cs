using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BudgettingApi.Data;

public class UserBudget
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public float BudgetPercentage { get; set; }
    public float CurrentSavedTotal {get; set;}
    public string Name { get; set; } = "";

    public virtual User User { get; set; } = null!;
}

public static class UserBudgetConverter
{
    public static UserBudgetDto ToDto(this UserBudget userBudget)
    {
        return new UserBudgetDto()
        {
            Name = userBudget.Name,
            Id = userBudget.Id,
            BudgetPercentage = userBudget.BudgetPercentage,
            CurrentSavedTotal = userBudget.CurrentSavedTotal
        };
    }
}

public class UserBudgetDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("percentage")]
    public float BudgetPercentage { get; set; }

    [JsonPropertyName("currentTotal")]
    public float CurrentSavedTotal { get; set; }
}