using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Interceptors;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Service;

namespace TalentConsulting.TalentSuite.Reports.API;

[ExcludeFromCodeCoverage]
public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        string useDbType = configuration.GetValue<string>("UseDbType");

        switch (useDbType)
        {
            default:
                builder.UseInMemoryDatabase("FH-LAHubDb");
                break;

            case "UseSqlServerDatabase":
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    if (connectionString != null)
                        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("TalentConsulting.TalentSuite.Reports.API"));

                }
                break;

            case "UsePostgresDatabase":
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    if (connectionString != null)
                        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly("TalentConsulting.TalentSuite.Reports.API"));

                }
                break;
        }

        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor = new(new CurrentUserService(new HttpContextAccessor()), new DateTimeService());

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        return new ApplicationDbContext(builder.Options,
#if USE_DISPATCHER
            null, 
#endif
            auditableEntitySaveChangesInterceptor);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}