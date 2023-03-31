using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Infrastructure;

public interface IApplicationDbContext
{
    public DbSet<Report> Reports { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
