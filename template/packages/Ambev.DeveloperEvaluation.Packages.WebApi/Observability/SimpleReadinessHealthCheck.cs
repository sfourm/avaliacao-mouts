using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Observability;

public class SimpleReadinessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}