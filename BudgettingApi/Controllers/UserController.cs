using System.Security.Claims;
using BudgettingApi.Configs;
using BudgettingApi.Data;
using BudgettingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Controllers;

[Authorize]
[ApiController]
[Route(ApiConsts.SecureRoute + "[controller]")]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet("getdata")]
    public async Task<IActionResult> GetData()
    {
        var user = await userService.GetUserFromClaims(User, user => user.Include(u => u.Budgets));
        return Ok(user.ToDto());
    }
}