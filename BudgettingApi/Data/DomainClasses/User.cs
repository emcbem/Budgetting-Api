namespace BudgettingApi.Data;

public class User 
{
    public string? Name {get; set;}
    public string Email {get; set;}= "";
    public int Id {get; set;}
    public string ProviderId {get; set;} = "";
}