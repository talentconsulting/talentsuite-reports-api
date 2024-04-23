using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class TestServer: WebApplicationFactory<Program>
{
    public IServiceScope GetServiceScope => Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    internal async Task ResetDbAsync(Func<IApplicationDbContext, Task> value)
    {
        if (value is null) return;

        using var scope = Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await ctx.Database.EnsureDeletedAsync();
        await ctx.Database.EnsureCreatedAsync();

        await value(ctx);
    }

    internal async Task QueryDbAsync(Func<IApplicationDbContext, Task> value)
    {
        if (value is null) return;

        using var scope = Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        await value(ctx);
    }
}