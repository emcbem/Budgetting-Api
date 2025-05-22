using System.Runtime.Serialization;
using GenerativeAI;

namespace BudgettingApi.Services;

public class AiService : IAiService
{
    private readonly IConfiguration configuration;

    public AiService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<T?> GetJsonResponse<T>(string request) where T : class
    {
        var googleAI = new GoogleAi(configuration.GetValue<string>("AI_KEY", ""));
        var model = googleAI.CreateGeminiModel("models/gemini-1.5-flash");
        var response = await model.GenerateObjectAsync<T>(request);
        return response;
    }
}