using Microsoft.Extensions.Diagnostics.HealthChecks;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class DefaultHealthCheckTests
{
    [Test]
    public async Task CheckHealthAsync_Returns_Healthy()
    {
        // arrange
        var healthCheckContext = new HealthCheckContext();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Ping(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        var sut = new DefaultHealthCheck(dbContext);

        // act
        var result = await sut.CheckHealthAsync(healthCheckContext);

        // assert
        result.Status.ShouldBe(HealthStatus.Healthy);
    }

    [Test]
    public async Task CheckHealthAsync_Returns_UnHealthy()
    {
        // arrange
        var healthCheckContext = new HealthCheckContext();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Ping(Arg.Any<CancellationToken>()).Returns(Task.FromException(new Exception()));
        var sut = new DefaultHealthCheck(dbContext);

        // act
        var result = await sut.CheckHealthAsync(healthCheckContext);

        // assert
        result.Status.ShouldBe(HealthStatus.Unhealthy);
    }
}
