using Ambev.DeveloperEvaluation.Infrastructure.EF;
using Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddEfCore(configuration, environment);
        services.AddRepositories();
        services.AddServices();
    }
}