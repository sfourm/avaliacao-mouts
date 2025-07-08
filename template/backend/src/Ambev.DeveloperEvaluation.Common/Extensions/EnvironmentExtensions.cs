using Microsoft.Extensions.Hosting;

namespace Ambev.DeveloperEvaluation.Common.Extensions;

/// <summary>
///     Provides extension methods for environment validation in ASP.NET Core applications.
///     These methods allow for checking different environment types (Development, Production, Staging, UAT, QA)
///     based on the ASPNETCORE_ENVIRONMENT variable, including alternative naming conventions.
///     All comparisons are case-insensitive.
///     Development can be 'Development' or 'trunk'
///     Production can be 'Production' or 'prod'
///     Staging can be 'Staging' or 'stg'
/// </summary>
public static class EnvironmentExtensions
{
    /// <summary>
    ///     Checks if the current environment is Development or Trunk based on ASPNETCORE_ENVIRONMENT variable.
    /// </summary>
    /// <returns>
    ///     <c>true</c> if the environment is "Development" or "trunk"; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     This method retrieves the value of the "ASPNETCORE_ENVIRONMENT" environment variable and
    ///     performs a case-insensitive comparison to check against "Development" or "trunk".
    /// </remarks>
    public static bool IsDevelopment()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.Equals(environment, Environments.Development, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///     Checks if the current environment is Development or Trunk based on ASPNETCORE_ENVIRONMENT variable.
    /// </summary>
    /// <returns>True if environment is Development or trunk, false otherwise.</returns>
    public static bool IsDevelopment(this IHostEnvironment env)
    {
        return env.EnvironmentName.Equals(Environments.Development, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///     Checks if the current environment is Production based on ASPNETCORE_ENVIRONMENT variable.
    /// </summary>
    /// <returns>True if environment is Production or prod, false otherwise.</returns>
    public static bool IsProduction(this IHostEnvironment env)
    {
        return env.EnvironmentName.Equals(Environments.Production, StringComparison.InvariantCultureIgnoreCase);
    }
}