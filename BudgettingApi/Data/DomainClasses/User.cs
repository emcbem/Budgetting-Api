using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BudgettingApi.Data;

public class User
{
    public string? Name { get; set; }
    public string Email { get; set; } = "";
    public int Id { get; set; }
    public string ProviderId { get; set; } = "";

    public virtual ICollection<UserBudget> Budgets { get; set; } = null!;
}

public static class UserConverter
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Email = user.Email,
            Name = user.Name,
            Budgets = user.Budgets?.Select(x => x.ToDto()).ToList() ?? []
        };
    }
}

public class UserDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("budgets")]
    public IEnumerable<UserBudgetDto>? Budgets { get; set; }
}