using System.Security.Claims;
using BudgettingApi.Data.Requests.Adds;
using BudgettingApi.Data.Requests.Updates;

namespace BudgettingApi.Services;

public interface IBudgetService
{
    public Task CreateBudgetForUser(BudgetRequest budgetRequest, ClaimsPrincipal User);
    public Task DeleteBudgetFromUser(int budgetId, ClaimsPrincipal User);
    public Task UpdateBudgetFromUser(UpdateBudgetRequest updateBudgetRequest, ClaimsPrincipal User);
}