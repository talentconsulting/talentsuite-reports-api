using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi;

[ExcludeFromCodeCoverage]
public static partial class WebApplicationExtensions
{
    static partial void RegisterEndpoints(this WebApplication app);

    public static async Task Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.ShowCommonExtensions();
            });
        }

        app.UseHttpsRedirection();
        app.MapHealthChecks("/health");
        await app.InitialiseDb();
        app.RegisterEndpoints();
    }

    private static async Task InitialiseDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

            if (!dbContext.Database.ProviderName?.Contains("InMemory", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                await dbContext.Database.MigrateAsync(CancellationToken.None);
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, ex.Message);
            throw;
        }
    }
}