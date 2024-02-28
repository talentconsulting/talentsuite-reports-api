using AutoMapper;
using MediatR;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Events;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;

public class CreateReportCommand : IRequest<string>, ICreateReportCommand
{
    public CreateReportCommand(ReportDto reportDto)
    {
        ReportDto = reportDto;
    }

    public ReportDto ReportDto { get; }
}

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateReportCommandHandler> _logger;
    public CreateReportCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateReportCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var unsavedEntity = _mapper.Map<Report>(request.ReportDto);
            ArgumentNullException.ThrowIfNull(unsavedEntity);

            var existing = _context.Reports.FirstOrDefault(e => unsavedEntity.Id == e.Id);

            if (existing is not null)
                throw new InvalidOperationException($"Report with Id: {unsavedEntity.Id} already exists, Please use Update command");

            unsavedEntity.Risks = AttachExistingRisk(unsavedEntity.Risks);

            unsavedEntity.RegisterDomainEvent(new ReportCreatedEvent(unsavedEntity));
            _context.Reports.Add(unsavedEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating a project. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request.ReportDto is not null)
            return request.ReportDto.Id;
        else
            return string.Empty;
    }

    private List<Risk> AttachExistingRisk(ICollection<Risk>? unSavedEntities)
    {
        var returnList = new List<Risk>();

        if (unSavedEntities is null || unSavedEntities.Count == 0)
            return returnList;

        var existing = _context.Risks.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.Find(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }
}

