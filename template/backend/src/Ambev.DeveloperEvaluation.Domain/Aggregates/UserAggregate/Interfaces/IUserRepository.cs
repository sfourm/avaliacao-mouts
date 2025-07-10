using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}