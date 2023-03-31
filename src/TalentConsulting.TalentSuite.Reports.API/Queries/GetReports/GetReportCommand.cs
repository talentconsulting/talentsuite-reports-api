using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;


public class GetReportCommand : IRequest<PaginatedList<ReportDto>>
{
    public GetReportCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}

public class GetReportCommandHandler : IRequestHandler<GetReportCommand, PaginatedList<ReportDto>>
{
    private readonly ApplicationDbContext _context;

    public GetReportCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<ReportDto>> Handle(GetReportCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Reports
            .Include(x => x.Risks)
            .Where(x => x.Id == request.Id);


        if (entities == null)
        {
            throw new NotFoundException(nameof(Report), "Reports");
        }

        var filteredReports = await entities.Select(x => new ReportDto(x.Id, (x.Created != null) ? x.Created.Value : DateTime.UtcNow, x.PlannedTasks, x.CompletedTasks, x.Weeknumber, x.SubmissionDate, x.ProjectId, x.UserId, EntityToDtoHelper.GetRisks(x.Risks))).ToListAsync();

        if (request != null)
        {
            var pageList = filteredReports.Skip((0 - 1) * 1).Take(1).ToList();
            var result = new PaginatedList<ReportDto>(pageList, filteredReports.Count, 1, 1);

            return result;
        }

        return new PaginatedList<ReportDto>(filteredReports, filteredReports.Count, 1, 10);
    }
}