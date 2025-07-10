using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.Packages.Security.ApplicationUser;

public interface IApplicationUserService
{
    bool IsAuthenticated { get; }
    string UserId { get; }
    string Name { get; }
    string Email { get; }
    IEnumerable<Claim> Claims { get; }
}