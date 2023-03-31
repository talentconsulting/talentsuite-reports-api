using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;


public class GetReportsCommand : IRequest<PaginatedList<ReportDto>>
{
    public GetReportsCommand(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber != null ? pageNumber.Value : 1;
        PageSize = pageSize != null ? pageSize.Value : 1;
    }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetReportsCommandHandler : IRequestHandler<GetReportsCommand, PaginatedList<ReportDto>>
{
    private readonly ApplicationDbContext _context;

    public GetReportsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<ReportDto>> Handle(GetReportsCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Reports
          .Include(x => x.Risks);

        if (entities == null)
        {
            throw new NotFoundException(nameof(Report), "Reports");
        }

        var filteredReports = await entities.Select(x => new ReportDto(x.Id, (x.Created != null) ? x.Created.Value : DateTime.UtcNow, x.PlannedTasks, x.CompletedTasks, x.Weeknumber, x.SubmissionDate, x.ProjectId, x.UserId, EntityToDtoHelper.GetRisks(x.Risks))).ToListAsync();

        if (request != null)
        {
            var pageList = filteredReports.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            var result = new PaginatedList<ReportDto>(pageList, filteredReports.Count, request.PageNumber, request.PageSize);

            return result;
        }

        return new PaginatedList<ReportDto>(filteredReports, filteredReports.Count, 1, 10);
    }
}


