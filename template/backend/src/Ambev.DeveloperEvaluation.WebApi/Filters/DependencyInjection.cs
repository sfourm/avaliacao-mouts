using Ambev.DeveloperEvaluation.Packages.WebApi.Filters;
using Ambev.DeveloperEvaluation.WebApi.Filters.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Filters;

public static class DependencyInjection
{
    public static void AddCustomExceptions(this IServiceCollection services)
    {
        services
            .AddExceptionHandler<DomainAlreadyExistsHandler>()
            .AddExceptionHandler<DomainNotFoundHandler>()
            .AddExceptionHandler<DomainRuleViolationHandler>();
    }
}