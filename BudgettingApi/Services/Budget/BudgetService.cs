using System.Security.Claims;
using BudgettingApi.Data;
using BudgettingApi.Data.Requests.Adds;
using BudgettingApi.Data.Requests.Updates;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Services;

public class BudgetService : IBudgetService
{
    private readonly IUserService userService;
    private readonly IDbContextFactory<BudgettingDbContext> dbContextFactory;

    public BudgetService(IUserService userService, IDbContextFactory<BudgettingDbContext> dbContextFactory)
    {
        this.userService = userService;
        this.dbContextFactory = dbContextFactory;
    }
    public async Task CreateBudgetForUser(BudgetRequest budgetRequest, ClaimsPrincipal User)
    {
        var enhancedRequest = (await userService.EnhanceRequestWithClaims(budgetRequest, User)) as BudgetRequest;

        var context = dbContextFactory.CreateDbContext();

        var newBudget = new UserBudget()
        {
            Name = enhancedRequest?.Name ?? throw new Exception("Budget cannot be made without a name"),
            UserId = enhancedRequest.UserId
        };

        context.UserBudgets.Add(newBudget);
        await context.SaveChangesAsync();
    }

    public async Task<UserBudget?> GetBudget(int id)
    {
        var context = dbContextFactory.CreateDbContext();

        return await context.UserBudgets.FirstOrDefaultAsync(ub => ub.Id == id);
    }

    public async Task DeleteBudgetFromUser(int budgetId, ClaimsPrincipal User)
    {
        var user = await userService.GetUserFromClaims(User);
        var budget = await GetBudget(budgetId);

        if(budget is null || user.Id != budget.UserId)
        {
            return;
        }

        var context = dbContextFactory.CreateDbContext();
        context.UserBudgets.Remove(budget);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBudgetFromUser(UpdateBudgetRequest updateBudgetRequest, ClaimsPrincipal User)
    {
        var user = await userService.GetUserFromClaims(User);
        var budget = await GetBudget(updateBudgetRequest.Id);

        if(budget is null || user.Id != budget.UserId)
        {
            return;
        }

        budget.BudgetPercentage = updateBudgetRequest.BudgetPercentage;
        budget.Name = updateBudgetRequest.Name;

        var context = dbContextFactory.CreateDbContext();
        context.Update(budget);
        await context.SaveChangesAsync();
    }
}