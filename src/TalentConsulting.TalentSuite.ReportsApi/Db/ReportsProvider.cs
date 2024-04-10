using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.ReportsApi.Common;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

internal record DeleteResult(bool Success, bool Found);

internal class ReportsProvider(IApplicationDbContext context) : IReportsProvider
{
    public async Task<bool> Delete(Guid reportId, CancellationToken cancellationToken)
    {
        var report = await context.Reports.FindAsync(reportId, cancellationToken);
        if (report == null)
        {
            return false;
        }

        // TODO: delete the dependent Risks
        context.Reports.Remove(report);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<PagedResults<Report>> GetAllBy(Guid projectId, PageQueryParameters pagingInfo)
    {
        var entities = context.Reports
            //.OrderBy(x => x.CreatedWhen)
            //.ThenBy(x => x.Id)
            .Where(x => x.ProjectId == projectId);
        
        var totalCount = await entities.CountAsync();

        var skip = (pagingInfo.SafePage - 1) * pagingInfo.SafePageSize;
        entities = entities
            .Skip(skip)
            .Take(pagingInfo.SafePageSize);

        var results = await entities.Include(x => x.Risks).ToListAsync();

        return new PagedResults<Report>(skip + 1, totalCount, results);
    }

    public async Task<Report?> Fetch(Guid reportId, CancellationToken cancellationToken)
    {
        return await context.Reports
            .Include(x => x.Risks)
            .FirstOrDefaultAsync(report => report.Id == reportId, cancellationToken);
    }

    public async Task Update(Report report, CancellationToken cancellationToken)
    {
        context.Reports.Attach(report);
        if (report.Risks is not null && report.Risks.Any())
        {
            // TODO: context.Risks.AttachRange()
            context.Risks.AttachRange(report.Risks);
            context.Risks.UpdateRange(report.Risks);
        }
        else
        {
            //context.Risks.Where(x => x.)
        }
        context.Reports.Update(report);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Report> Create(Report report, CancellationToken cancellationToken)
    {
        await context.Reports.AddAsync(report, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return report;
    }
}