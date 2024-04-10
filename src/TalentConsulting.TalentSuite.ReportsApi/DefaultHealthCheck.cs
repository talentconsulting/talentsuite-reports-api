using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TalentConsulting.TalentSuite.ReportsApi;

public class DefaultHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // TODO: ping the db
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}