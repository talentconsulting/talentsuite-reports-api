using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateReport;

public class UpdateReportCommand : IRequest<string>, IUpdateReportCommand
{
    public UpdateReportCommand(string id, ReportDto reportDto)
    {
        Id = id;
        ReportDto = reportDto;
    }

    public string Id { get; }
    public ReportDto ReportDto { get; }
}

public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateReportCommandHandler> _logger;
    public UpdateReportCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateReportCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Reports.AsNoTracking()
            .Include(x => x.Risks)
            .FirstOrDefault(x => x.Id.ToString() == request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Report), request.Id);
        }

        try
        {
            _mapper.Map(request.ReportDto, entity);
            ArgumentNullException.ThrowIfNull(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating Report. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.ReportDto is not null)
            return request.ReportDto.Id;
        else
            return string.Empty;
    }
}


