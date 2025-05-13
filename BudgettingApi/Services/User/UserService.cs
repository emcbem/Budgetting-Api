using System.Security.Claims;
using BudgettingApi.Data;
using BudgettingApi.Data.Requests;
using Microsoft.EntityFrameworkCore;

namespace BudgettingApi.Services;

public class UserService : IUserService
{
    private readonly IDbContextFactory<BudgettingDbContext> dbContextFactory;

    public UserService(IDbContextFactory<BudgettingDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<User> GetUserFromClaims(ClaimsPrincipal User, Func<IQueryable<User>, IQueryable<User>>? includeFunction = null)
    {
        var providerId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? throw new Exception("Unable to parse user data");
        var email = User.FindFirst("https://budgetting/email")?.Value ?? throw new Exception("Unable to parse user data");

        var db = dbContextFactory.CreateDbContext();

        includeFunction ??= (user => user);
        var user = await includeFunction(db.Users).FirstOrDefaultAsync(u => u.ProviderId == providerId);

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

        return user;
    }

    public async Task<IUserRequest> EnhanceRequestWithClaims(IUserRequest request, ClaimsPrincipal User)
    {
        var user = await GetUserFromClaims(User);
        request.UserId = user.Id;
        return request;
    }
}