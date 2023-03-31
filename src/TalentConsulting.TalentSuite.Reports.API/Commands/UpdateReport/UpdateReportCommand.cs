using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
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
        var entity = _context.Reports.FirstOrDefault(x => x.id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Report), request.Id);
        }

        try
        {
            entity.PlannedTasks = request.ReportDto.PlannedTasks;
            entity.CompletedTasks = request.ReportDto.CompletedTasks;
            entity.Weeknumber = request.ReportDto.Weeknumber;
            entity.ProjectId = request.ReportDto.ProjectId;
            entity.SubmissionDate = request.ReportDto.SubmissionDate;
            entity.UserId = request.ReportDto.UserId;
            entity.Risks = AttachExistingRisks(request.ReportDto.Risks);

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

    private ICollection<Risk> AttachExistingRisks(ICollection<RiskDto>? unSavedEntities)
    {
        var returnList = new List<Risk>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Risks.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.id == unSavedItem.Id);

            if (savedItem is not null)
            {
                savedItem.ReportId = unSavedItem.ReportId;
                savedItem.RiskDetails = unSavedItem.RiskDetails;
                savedItem.RiskMitigation = unSavedItem.RiskMitigation;
                savedItem.RagStatus = unSavedItem.RagStatus;
            }

            returnList.Add(savedItem ?? _mapper.Map<Risk>(unSavedItem));
        }

        return returnList;
    }
}


