using BudgettingApi.Configs;
using BudgettingApi.Data.Requests.Adds;
using BudgettingApi.Data.Requests.Updates;
using BudgettingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgettingApi.Controllers;

[Authorize]
[ApiController]
[Route(ApiConsts.SecureRoute + "[controller]")]
public class BudgetController : Controller
{
    private readonly IBudgetService budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        this.budgetService = budgetService;
    }

    [HttpPost("createBudget")]
    public async Task<IActionResult> CreateBudget(BudgetRequest budgetRequest)
    {
        await budgetService.CreateBudgetForUser(budgetRequest, User);
        return Ok();
    }

    [HttpDelete("deleteBudget/{budgetId}")]
    public async Task<IActionResult> DeleteBudget(int budgetId)
    {
        await budgetService.DeleteBudgetFromUser(budgetId, User);
        return Ok();
    }

    [HttpPatch("updateBudget")]
    public async Task<IActionResult> UpdateBudget(UpdateBudgetRequest updateBudgetRequest)
    {
        await budgetService.UpdateBudgetFromUser(updateBudgetRequest, User);
        return Ok();
    }
}

