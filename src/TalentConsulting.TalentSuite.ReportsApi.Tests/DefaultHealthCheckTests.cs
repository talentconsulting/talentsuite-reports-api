using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class DefaultHealthCheckTests
{
    private DbContext _dbContext;
    private DatabaseFacade _database;
    private IApplicationDbContext _appDbContext;

    [SetUp]
    public void Setup()
    {
        _dbContext = Substitute.For<DbContext>();
        _database = Substitute.For<DatabaseFacade>(_dbContext);
        _appDbContext = Substitute.For<IApplicationDbContext>();
        _appDbContext.Database.Returns(_database);
        _appDbContext.Ping(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
    }

    [TearDown]
    public void Teardown()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task CheckHealthAsync_Returns_Healthy_When_Ping_Ok()
    {
        // arrange
        _database.ProviderName.Returns("Something");
        _appDbContext.Ping(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        var sut = new DefaultHealthCheck(_appDbContext);

        // act
        var result = await sut.CheckHealthAsync(new HealthCheckContext());

        // assert
        result.Status.ShouldBe(HealthStatus.Healthy);
        await _appDbContext.Received(1).Ping(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CheckHealthAsync_Returns_UnHealthy_When_Ping_Not_Ok()
    {
        // arrange
        _database.ProviderName.Returns("Something");
        _appDbContext.Ping(Arg.Any<CancellationToken>()).Returns(Task.FromException(new Exception()));
        var sut = new DefaultHealthCheck(_appDbContext);

        // act
        var result = await sut.CheckHealthAsync(new HealthCheckContext());

        // assert
        result.Status.ShouldBe(HealthStatus.Unhealthy);
        await _appDbContext.Received(1).Ping(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CheckHealthAsync_Returns_Healthy_When_InMemory()
    {
        // arrange
        _database.ProviderName.Returns("Microsoft.EntityCore.InMemory");
        var sut = new DefaultHealthCheck(_appDbContext);

        // act
        var result = await sut.CheckHealthAsync(new HealthCheckContext());

        // assert
        result.Status.ShouldBe(HealthStatus.Healthy);
        await _appDbContext.Received(0).Ping(Arg.Any<CancellationToken>());
    }
}
