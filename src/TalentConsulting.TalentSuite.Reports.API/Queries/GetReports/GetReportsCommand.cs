using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
    private readonly IMapper _mapper;

    public GetReportsCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ReportDto>> Handle(GetReportsCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Reports
          .Include(x => x.Risks);
          

        if (entities == null)
        {
            throw new NotFoundException(nameof(Report), "Reports");
        }

        List<ReportDto> pagelist;
        if (request != null)
        {
            pagelist = await entities.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
                 .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();
            return new PaginatedList<ReportDto>(pagelist, pagelist.Count, request.PageNumber, request.PageSize);
        }

        pagelist = _mapper.Map<List<ReportDto>>(entities);
        var result = new PaginatedList<ReportDto>(pagelist.ToList(), pagelist.Count, 1, 10);
        return result;
    }
}


