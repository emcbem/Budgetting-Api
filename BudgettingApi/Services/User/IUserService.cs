using System.Security.Claims;
using BudgettingApi.Data;
using BudgettingApi.Data.Requests;

namespace BudgettingApi.Services;

public interface IUserService {
    public Task<User> GetUserFromClaims (ClaimsPrincipal User, Func<IQueryable<User>, IQueryable<User>>? includeFunction = null);
    public Task<IUserRequest> EnhanceRequestWithClaims(IUserRequest request, ClaimsPrincipal User);

}