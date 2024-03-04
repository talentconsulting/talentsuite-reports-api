using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;


public class GetReportCommand : IRequest<ReportDto>
{
    public GetReportCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}

public class GetReportCommandHandler : IRequestHandler<GetReportCommand,ReportDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReportCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ReportDto> Handle(GetReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Reports
            .Include(x => x.Risks)
            .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


        if (entity == null)
        {
            throw new NotFoundException(nameof(ReportDto), request.Id.ToString());
        }

        return entity;

    }
}