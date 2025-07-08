using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;

namespace Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(IUser user);
    }
}
