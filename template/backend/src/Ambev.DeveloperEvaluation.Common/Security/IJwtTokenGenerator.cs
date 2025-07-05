using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;

namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser user);
    }
}
