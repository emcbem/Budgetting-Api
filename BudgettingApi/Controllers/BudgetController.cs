using BudgettingApi.Configs;
using Microsoft.AspNetCore.Mvc;

namespace BudgettingApi.Controllers;

[ApiController]
[Route(ApiConsts.SecureRoute + "[controller]")]
public class BudgetController : Controller
{
    public BudgetController(IBudgetService budgetService)
    {
        
    }
}

