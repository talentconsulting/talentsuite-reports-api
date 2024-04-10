using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

public interface IApplicationDbContext
{
    public DbSet<Report> Reports { get; }
    public DbSet<Risk> Risks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
