using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;
using TalentConsulting.TalentSuite.Reports.Core.Infrastructure;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Interceptors;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Service;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var useDbType = configuration.GetValue<string>("UseDbType");

        switch (useDbType)
        {
            case "UseInMemoryDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ReferralDb"));
                break;

            case "UseSqlServerDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ReferralConnection") ?? String.Empty));
                break;

            case "UsePostgresDatabase":
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ReferralConnection") ?? String.Empty));
                break;

            default:
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ReferralDb"));
                break;
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
