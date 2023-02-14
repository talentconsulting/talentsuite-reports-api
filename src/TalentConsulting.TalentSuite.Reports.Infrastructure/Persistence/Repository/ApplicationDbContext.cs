using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Infrastructure;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Interceptors;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventDispatcher dispatcher,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            IConfiguration configuration
        )
        : base(options)
    {
        _dispatcher = dispatcher;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as EntityBase<string>)
            .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
            .ToArray();


#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        if (entitiesWithEvents != null && entitiesWithEvents.Any())
        {
            //var entitiesWithEventsGuids = new List<EntityBase<Guid>>();
            //foreach (var entityWithEvents in entitiesWithEvents)
            //{
            //    var t = entityWithEvents?.ToString();
            //    entitiesWithEventsGuids.Add(Guid.Parse(t);
            //}

            //await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        }

#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        return result;
    }

    public DbSet<Audit> Audits => Set<Audit>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ClientProject> ClientProjects => Set<ClientProject>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Recipient> Recipients => Set<Recipient>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Risk> Risks => Set<Risk>();
    public DbSet<Sow> Sows => Set<Sow>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserGroup> UserGroups => Set<UserGroup>();
    public DbSet<UserProjectRole> UserProjectRoles => Set<UserProjectRole>();
}
