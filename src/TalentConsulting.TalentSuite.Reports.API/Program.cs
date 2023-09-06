
using Serilog;

namespace TalentConsulting.TalentSuite.Reports.API;

public class Program
{
    protected Program() { }
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    private const string AllowReactAppForLocalDev = "AllowReactAppForLocalDev";

    public static async Task Main(string[] args)
    {
        

        try
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

            Log.Information("Starting up");

            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureHost();

            builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsProduction());

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowReactAppForLocalDev,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            ServiceProvider = await app.ConfigureWebApplication();

            app.UseCors(AllowReactAppForLocalDev);

            await app.RunAsync();
        }
        catch (Exception e)
        {
            string type = e.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(e, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

