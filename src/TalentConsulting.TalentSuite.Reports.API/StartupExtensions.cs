using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;
using TalentConsulting.TalentSuite.Reports.API.Endpoints;
using TalentConsulting.TalentSuite.Reports.Infrastructure;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API;

public static class StartupExtensions
{
    public static void ConfigureHost(this WebApplicationBuilder builder)
    {
        // ApplicationInsights
        builder.Host.UseSerilog((_, services, loggerConfiguration) =>
        {
            var logLevelString = builder.Configuration["LogLevel"];

            var parsed = Enum.TryParse<LogEventLevel>(logLevelString, out var logLevel);

            loggerConfiguration.WriteTo.ApplicationInsights(
                services.GetRequiredService<TelemetryConfiguration>(),
                TelemetryConverter.Traces,
                parsed ? logLevel : LogEventLevel.Warning);

            loggerConfiguration.WriteTo.Console(
                parsed ? logLevel : LogEventLevel.Warning);
        });
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {
        services.AddApplicationInsightsTelemetry();

        // Adding Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            // Adding Jwt Bearer
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration["JWT:Secret"] ?? "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"))
            };
        });

        //https://www.youtube.com/watch?v=cbtK3U2aOlg
        services.AddAuthorization(options =>
        {
            if (isProduction)
            {
                options.AddPolicy("TalentConsultingUser", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("TalentConsultingReader") ||
                        context.User.IsInRole("TalentConsultingWriter")));
            }
            else //LocalHost, Dev, Test, PP, disable Authorisation
            {
                options.AddPolicy("TalentConsultingUser", policy =>
                    policy.RequireAssertion(_ => true));
            }
        });

        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer()
            .AddInfrastructureServices(configuration)
            .AddApplicationServices();

        services.AddTransient<MinimalGeneralEndPoints>();
        services.AddTransient<MinimalReportEndPoints>();
        services.AddTransient<ApplicationDbContextInitialiser>();

        services.AddSwaggerGen();
    }

    public static async Task<IServiceProvider> ConfigureWebApplication(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RegisterEndPoints();

        return app.Services;
    }

    private static async Task RegisterEndPoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var genapi = scope.ServiceProvider.GetService<MinimalGeneralEndPoints>();
        genapi?.RegisterMinimalGeneralEndPoints(app);

        var reportsApi = scope.ServiceProvider.GetService<MinimalReportEndPoints>();
        reportsApi?.RegisterReportEndPoints(app);

        try
        {
            if (!app.Environment.IsProduction())
            {
                // Seed Database
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                var shouldResetDatabase = app.Configuration.GetValue<bool>("RestartDatabase");
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
