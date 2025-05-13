using System.Security.Claims;
using BudgettingApi.Configs;
using BudgettingApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Controllers;

[Authorize]
[ApiController]
[Route(ApiConsts.SecureRoute + "[controller]")]
public class UserController : Controller
{
    private readonly IDbContextFactory<BudgettingDbContext> _dbFactory;

    public UserController(IDbContextFactory<BudgettingDbContext> db)
    {
        _dbFactory = db;
    }
    [HttpGet("getdata")]
    public async Task<IActionResult> GetData()
    {
        System.Console.WriteLine("here");
        var providerId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? throw new Exception("Unable to parse user data");
        var email = User.FindFirst("https://budgetting/email")?.Value ?? throw new Exception("Unable to parse user data");
        var db = _dbFactory.CreateDbContext();

        var user = await db.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId);
        if (user == null)
        {
            user = new User
            {
                ProviderId = providerId,
                Email = email,
            };
            db.Users.Add(user);

            await db.SaveChangesAsync();
        }
        Console.WriteLine("User retriveed");

        return Ok(user);
    }
}