using System.Reflection;
using Ambev.DeveloperEvaluation.Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Common;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationBehaviours(this IServiceCollection services, Assembly assembly,
        Action<MediatRServiceConfiguration>? mediator = null)
    {
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            mediator?.Invoke(configuration);
        });

        services.AddHttpContextAccessor();

        return services;
    }
}