using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters;

/// <summary>
///     Provides extension methods for registering exception handlers in an <see cref="IServiceCollection" />.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers a set of default exception handlers in the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the exception handlers to.</param>
    /// <returns>The <see cref="IServiceCollection" /> with the registered exception handlers.</returns>
    /// <remarks>
    ///     This method registers a predefined list of exception handlers, which handle various common exceptions
    ///     that may occur within the application. The handlers include:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>
    ///                 <see cref="AlreadyExistsExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="BadRequestExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="BusinessRuleViolationExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="EntityValidationExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="ForbiddenAccessExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="InvalidEntityExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="NotFoundExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="QuotaExceededExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="SemanticValidationExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="UnauthorizedAccessExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="ValidationExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="HttpRequestExceptionHandler" />
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see cref="InvalidRequestExceptionHandler" />
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    internal static IServiceCollection AddDefaultExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<AlreadyExistsExceptionHandler>()
            .AddExceptionHandler<BadRequestExceptionHandler>()
            .AddExceptionHandler<BusinessRuleViolationExceptionHandler>()
            .AddExceptionHandler<EntityValidationExceptionHandler>()
            .AddExceptionHandler<ForbiddenAccessExceptionHandler>()
            .AddExceptionHandler<InvalidEntityExceptionHandler>()
            .AddExceptionHandler<NotFoundExceptionHandler>()
            .AddExceptionHandler<QuotaExceededExceptionHandler>()
            .AddExceptionHandler<SemanticValidationExceptionHandler>()
            .AddExceptionHandler<UnauthorizedAccessExceptionHandler>()
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<HttpRequestExceptionHandler>()
            .AddExceptionHandler<InvalidRequestExceptionHandler>();

        return services;
    }

    /// <summary>
    ///     Registers a specific exception handler of type <typeparamref name="T" /> in the specified
    ///     <see cref="IServiceCollection" />.
    /// </summary>
    /// <typeparam name="T">The type of the exception handler to register. Must implement <see cref="IExceptionHandler" />.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the exception handler to.</param>
    /// <returns>The <see cref="IServiceCollection" /> with the registered exception handler.</returns>
    public static IServiceCollection AddExceptionHandler<T>(this IServiceCollection services)
        where T : class, IExceptionHandler
    {
        services.AddScoped<IExceptionHandler, T>();
        return services;
    }
}