using System.Security.Claims;
using BudgettingApi.Data;
using BudgettingApi.Data.Requests.Adds;
using BudgettingApi.Data.Requests.Updates;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Services;

public class BudgetService : IBudgetService
{
    private readonly IUserService userService;
    private readonly IAiService aiService;
    private readonly IDbContextFactory<BudgettingDbContext> dbContextFactory;

    public BudgetService(IUserService userService, IAiService aiService, IDbContextFactory<BudgettingDbContext> dbContextFactory)
    {
        this.userService = userService;
        this.aiService = aiService;
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

        if (budget is null || user.Id != budget.UserId)
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

        if (budget is null || user.Id != budget.UserId)
        {
            return;
        }

        budget.BudgetPercentage = updateBudgetRequest.BudgetPercentage;
        budget.Name = updateBudgetRequest.Name;

        var context = dbContextFactory.CreateDbContext();
        context.Update(budget);
        await context.SaveChangesAsync();
    }

    public async Task<AiBudgetRequest> GetUserCurratedAiBudgets(string usersConcerns)
    {
        var request = $"""
            You are the best budgetting advisor. We need your help to give the users the best budget that they could ever want. 
            You know that savings are more important than material objects and your future is important. 
            But we also want the users to have a fun time in life. Make sure these budgets match the users concers. 

            Your response will include a general analysis of the decisions you made and why you made them. 
            It will also include a list of all the budgets that you feel this user should have. 
            It will include a name, decription of why, and the percentage of your income it should be taking each paycheck. 
            Remeber, it is percentage so that means that it is NOT percent. Do not try and make it add up to 1 in the end. 
            Make it add up to 100. DO NOT FORGET TO INCLUDE A PERCENTAGE IN EACH BUDGET.

            Remember that when a yearly salary is given, you need to divide it by 12 to get the montly cost.

            Here are the concerns of the current user, give them your best

            {usersConcerns}
        """;

        var budgetRequest = await aiService.GetJsonResponse<AiBudgetRequest>(request);
        return budgetRequest ?? new();
    }

    public async Task AcceptAiResponse(AiBudgetRequest aiBudgetRequest, ClaimsPrincipal user)
    {
        var userAccount = await userService.GetUserFromClaims(user);

        var budgetsToCreate = aiBudgetRequest.AiBudgets.Select(budget => new UserBudget() { BudgetPercentage = budget.IncomePercentage, Name = budget.Name, UserId = userAccount.Id }).ToList();

        var context = await dbContextFactory.CreateDbContextAsync();

        context.UserBudgets.AddRange(budgetsToCreate);
        await context.SaveChangesAsync();
    }
}