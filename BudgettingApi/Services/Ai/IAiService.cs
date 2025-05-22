namespace BudgettingApi;

public interface IAiService
{
    public Task<T?> GetJsonResponse<T>(string request) where T : class;
}