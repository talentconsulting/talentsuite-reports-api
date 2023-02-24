using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Infrastructure;

public interface IApplicationDbContext
{
    public DbSet<Audit> Audits { get; }
    public DbSet<Client> Clients { get; }
    public DbSet<ClientProject> ClientProjects { get; }
    public DbSet<Contact> Contacts { get; }
    public DbSet<Notification> Notifications { get; }
    public DbSet<Project> Projects { get; }
    DbSet<ProjectRole> ProjectRoles { get; }
    public DbSet<Recipient> Recipients { get; }
    public DbSet<Report> Reports { get; }
    public DbSet<Risk> Risks { get; }
    public DbSet<Sow> Sows { get; }
    public DbSet<User> Users { get; }
    public DbSet<UserGroup> UserGroups { get; }
    public DbSet<UserProjectRole> UserProjectRoles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
