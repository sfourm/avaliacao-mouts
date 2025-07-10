using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Packages.Security.ApplicationUser;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value ?? string.Empty;
    public string Name => _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    public IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
}