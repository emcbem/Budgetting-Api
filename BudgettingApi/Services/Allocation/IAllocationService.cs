using System.Security.Claims;

namespace BudgettingApi.Services;

public interface IAllocationService {
    public Task AllocateMoneyToUsersBudgets(float totalToAllocate, ClaimsPrincipal User);

}