using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.ReportsApi.Common;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

internal class ReportsProvider(IApplicationDbContext context) : IReportsProvider
{
    public async Task<Report> Create(Report report, CancellationToken cancellationToken)
    {
        context.Reports.Add(report);
        await context.SaveChangesAsync(cancellationToken);

        return report;
    }

    public async Task<Report?> Fetch(Guid reportId, CancellationToken cancellationToken)
    {
        return await context.Reports.FindAsync([reportId], cancellationToken: cancellationToken);
    }

    public async Task<bool> Delete(Guid reportId, CancellationToken cancellationToken)
    {
        var report = await Fetch(reportId, cancellationToken);
        if (report is null)
        {
            return false;
        }

        foreach (var risk in report.Risks)
        {
            context.Remove(risk);
        }
        context.Remove(report);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<Report?> Update(Report report, CancellationToken cancellationToken)
    {
        var existingReport = await Fetch(report.Id, cancellationToken);
        if (existingReport is null)
        {
            return null;
        }

        context.Entry(existingReport).CurrentValues.SetValues(report);
        foreach (var risk in report.Risks)
        {
            var existingRisk = existingReport.Risks.FirstOrDefault(r => r.Id == risk.Id);
            if (existingRisk is null)
            {
                existingReport.Risks.Add(risk);
            }
            else
            {
                context.Entry(existingRisk).CurrentValues.SetValues(risk);
            }
        }

        foreach (var risk in existingReport.Risks)
        {
            if (!report.Risks.Any(r => r.Id == risk.Id))
            {
                context.Remove(risk);
            }
        }

        var transaction = context.Database.BeginTransaction();
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return existingReport;
    }

    public async Task<PagedResults<Report>> FetchAllBy(Guid projectId, PageQueryParameters pagingInfo, CancellationToken cancellationToken)
    {
        var entities = context.Reports.Where(x => x.ProjectId == projectId);
        var totalCount = await entities.CountAsync(cancellationToken);
        
        var maxPage = (int)Math.Ceiling((double)totalCount / pagingInfo.SafePageSize);
        var actualPage = Math.Max(1, Math.Min(pagingInfo.SafePage, maxPage));
        var skip = (actualPage - 1) * pagingInfo.SafePageSize;
        
        entities = entities
            .Skip(skip)
            .Take(pagingInfo.SafePageSize);

        var results = await entities.Include(x => x.Risks).ToListAsync(cancellationToken);
        var first = totalCount > 0 ? skip + 1 : 0;
        var last = Math.Max(first + results.Count - 1, 0);
        return new PagedResults<Report>(actualPage, pagingInfo.SafePageSize, first, last, totalCount, results);
    }
}