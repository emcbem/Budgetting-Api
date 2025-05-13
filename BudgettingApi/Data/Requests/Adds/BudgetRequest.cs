using System.Text.Json.Serialization;

namespace BudgettingApi.Data.Requests.Adds;

public class BudgetRequest : IUserRequest {
    [JsonPropertyName("name")]
    public string Name {get; set;} = "";
    public int UserId {get; set;}
}