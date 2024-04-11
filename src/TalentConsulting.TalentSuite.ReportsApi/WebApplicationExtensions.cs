using Serilog;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi;

static partial class WebApplicationExtensions
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
            if (!app.Environment.IsProduction())
            {
                // Seed Database
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                var shouldResetDatabase = app.Configuration.GetValue<bool>("RestartDatabase", false);
                await initialiser.InitialiseAsync(app.Environment.IsProduction(), shouldResetDatabase);
                await initialiser.SeedAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
        }
    }
}