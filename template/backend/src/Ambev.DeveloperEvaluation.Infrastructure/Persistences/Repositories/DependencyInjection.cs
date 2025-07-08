using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories;

public static class DependencyInjection
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}