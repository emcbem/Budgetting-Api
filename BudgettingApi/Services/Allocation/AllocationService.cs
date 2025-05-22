using System.Security.Claims;
using BudgettingApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Services;

public class AllocationService : IAllocationService
{
    private readonly IUserService userService;
    private readonly IDbContextFactory<BudgettingDbContext> dbContextFactory;

    public AllocationService(IUserService userService, IDbContextFactory<BudgettingDbContext> dbContextFactory)
    {
        this.userService = userService;
        this.dbContextFactory = dbContextFactory;
    }
    public async Task AllocateMoneyToUsersBudgets(float totalToAllocate, ClaimsPrincipal User)
    {
        var user = await userService.GetUserFromClaims(User, (u => u.Include(u2 => u2.Budgets)));

        foreach (var budget in user.Budgets)
        {
            budget.CurrentSavedTotal += totalToAllocate * (budget.BudgetPercentage / 100);
        }

        var context = dbContextFactory.CreateDbContext();

        context.UpdateRange(user.Budgets);
        await context.SaveChangesAsync();
    }
}