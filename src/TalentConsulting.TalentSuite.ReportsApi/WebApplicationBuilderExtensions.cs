using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi;

internal static class WebApplicationBuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports API", Version = "v1" });
            options.DocumentFilter<HealthChecksFilter>();
        });
        builder.ConfigureSerilog();
        builder.Services.AddApplicationInsightsTelemetry();
        builder.ConfigureAuth();
        builder.ConfigureCors();
        builder.ConfigureEntityFramework();
        builder.ConfigureApplicationDependencies();
        builder.ConfigureHealthChecks();
    }

    private static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
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

    private static void ConfigureAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
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
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:Secret"] ?? "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"))
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.AddPolicy("TalentConsultingUser", policy =>
                    policy.RequireAssertion(_ => true));
            }
            else
            {
                options.AddPolicy("TalentConsultingUser", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("TalentConsultingReader") ||
                        context.User.IsInRole("TalentConsultingWriter")));
            }
        });
    }

    private static void ConfigureCors(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactAppForLocalDev", policy => {
                    policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                });
            });
        }
    }

    private static void ConfigureEntityFramework(this WebApplicationBuilder builder)
    {
        var useDbType = builder.Configuration.GetValue<string>("UseDbType");

        switch (useDbType)
        {
            case "UseInMemoryDatabase":
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TalentDb"));
                break;

            //case "UseSqlServerDatabase":
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? String.Empty));
            //    break;

            //case "UseSqlLite":
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? String.Empty));
            //    break;

            //case "UsePostgresDatabase":
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection") ?? String.Empty)
            //        .ReplaceService<ISqlGenerationHelper, NpgsqlSqlGenerationLowercasingHelper>());
            //    break;

            default:
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TalentDb"));
                break;
        }

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }

    private static void ConfigureApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ApplicationDbContextInitialiser>();
        builder.Services.AddTransient<IReportsProvider, ReportsProvider>();

        // Validators
        builder.Services.AddScoped<IValidator<CreateReportDto>, CreateReportDtoValidator>();
        builder.Services.AddScoped<IValidator<CreateRiskDto>, CreateRiskDtoValidator>();
        builder.Services.AddScoped<IValidator<ReportDto>, ReportDtoValidator>();
        builder.Services.AddScoped<IValidator<RiskDto>, RiskDtoValidator>();
    }

    private static void ConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddCheck<DefaultHealthCheck>("default");
    }
}