using System.Security.Claims;

namespace warehouse.Services.IRepositories
{
    public interface IUserContextServices
    {
        ClaimsPrincipal GetUser { get; }

        int GetUserId();
    }
}