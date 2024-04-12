using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

[ExcludeFromCodeCoverage]
internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Report>().OwnsMany(x => x.Risks, i =>
        {
            i.WithOwner().HasForeignKey("ReportId");
            i.Property<Guid>("Id");
            i.HasKey("Id");
        });
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }

    public async Task Ping(CancellationToken cancellationToken)
    {
        await Database
                .ExecuteSqlRawAsync("SELECT 1;", cancellationToken)
                .ConfigureAwait(false);
    }

    public DbSet<Report> Reports => Set<Report>();
}
