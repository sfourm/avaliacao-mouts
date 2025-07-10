using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories.Users;

public class UserRepository(DefaultContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Users
            .FirstOrDefaultAsync(u => u.Email.Address == email, cancellationToken);
    }
}