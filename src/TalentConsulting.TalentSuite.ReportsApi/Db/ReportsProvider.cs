﻿using Microsoft.EntityFrameworkCore;
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

        await context.SaveChangesAsync(cancellationToken);
        return existingReport;
    }

    public async Task<Report> Create(Report report, CancellationToken cancellationToken)
    {
        context.Reports.Add(report);
        await context.SaveChangesAsync(cancellationToken);

        return report;
    }
}