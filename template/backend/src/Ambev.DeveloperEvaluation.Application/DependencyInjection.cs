using System.Reflection;
using Ambev.DeveloperEvaluation.Packages.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Application;

public static class DependencyInjection
{
    public static void RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.RegisterApplicationBehaviours(Assembly.GetExecutingAssembly());
    }
}