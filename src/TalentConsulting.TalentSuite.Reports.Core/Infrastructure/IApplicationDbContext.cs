using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
