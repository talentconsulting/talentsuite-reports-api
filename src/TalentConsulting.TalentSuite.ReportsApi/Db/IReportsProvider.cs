using TalentConsulting.TalentSuite.ReportsApi.Common;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

internal interface IReportsProvider
{
    Task<PagedResults<Report>> GetAllBy(Guid projectId, PageQueryParameters pagingInfo);
    Task<bool> Delete(Guid reportId, CancellationToken cancellationToken);
    Task<Report?> Fetch(Guid reportId, CancellationToken cancellationToken);
    Task<Report?> Update(Report report, CancellationToken cancellationToken);
    Task<Report> Create(Report report, CancellationToken cancellationToken);
}
