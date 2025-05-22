using BudgettingApi.Configs;
using BudgettingApi.Data;
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

    public sealed record UserConcerns
    {
        public string userConcerns { get; set; } = "";
    }

    [HttpPost("aiBudgetRequest")]
    public async Task<IActionResult> GetAiBudgetRequest(UserConcerns userConcerns)
    {
        var resopnse = await budgetService.GetUserCurratedAiBudgets(userConcerns.userConcerns);
        return Ok(resopnse);
    }

    [HttpPost("acceptaibudget")]
    public async Task<IActionResult> AcceptAiResponse(AiBudgetRequest aiBudgetRequest)
    {
        await budgetService.AcceptAiResponse(aiBudgetRequest, User);
        return Ok();
    }
}

