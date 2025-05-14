using BudgettingApi.Configs;
using BudgettingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgettingApi.Controllers;

[Authorize]
[ApiController]
[Route(ApiConsts.SecureRoute + "[controller]")]
public class AllocationController : Controller {
    private readonly IAllocationService allocationService;

    public AllocationController(IAllocationService allocationService)
    {
        this.allocationService = allocationService;
    }

    [HttpPost("submit/{payTotal}")]
    public async Task<IActionResult> SubmitPay(float payTotal)
    {
        await allocationService.AllocateMoneyToUsersBudgets(payTotal, User);
        return Ok();
    }
}