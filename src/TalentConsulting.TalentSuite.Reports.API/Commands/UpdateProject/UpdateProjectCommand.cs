using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<string>, IUpdateProjectCommand
{
    public UpdateProjectCommand(string id, ProjectDto projectDto)
    {
        Id = id;
        ProjectDto = projectDto;
    }

    public string Id { get; }
    public ProjectDto ProjectDto { get; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;
    public UpdateProjectCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateProjectCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Projects.FirstOrDefault(x => x.Id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        try
        {
            entity.ContactNumber = request.ProjectDto.ContactNumber;
            entity.Name = request.ProjectDto.Name;
            entity.Reference = request.ProjectDto.Reference;
            entity.StartDate = request.ProjectDto.StartDate;
            entity.EndDate = request.ProjectDto.EndDate;

            entity.ClientProjects = AttachExistingClientProjects(request.ProjectDto.ClientProjects);
            entity.Contacts = AttachExistingContacts(request.ProjectDto.Contacts);
            entity.Reports = AttachExistingReports(request.ProjectDto.Reports);
            entity.Sows = AttachExistingSows(request.ProjectDto.Sows);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating project. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.ProjectDto is not null)
            return request.ProjectDto.Id;
        else
            return string.Empty;
    }

    private ICollection<ClientProject> AttachExistingClientProjects(ICollection<ClientProjectDto>? unSavedEntities)
    {
        var returnList = new List<ClientProject>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.ClientProjects.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

            if (savedItem is not null)
            {
                savedItem.ClientId = unSavedItem.ClientId;
                savedItem.ProjectId = unSavedItem.ClientId;

            }

            returnList.Add(savedItem ?? _mapper.Map<ClientProject>(unSavedItem));

        }

        return returnList;
    }

    private ICollection<Contact> AttachExistingContacts(ICollection<ContactDto>? unSavedEntities)
    {
        var returnList = new List<Contact>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Contacts.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

            if (savedItem is not null)
            {
                savedItem.Firstname = unSavedItem.Firstname;
                savedItem.Email = unSavedItem.Email;
                savedItem.ReceivesReport = unSavedItem.ReceivesReport;
                savedItem.ProjectId = unSavedItem.ProjectId;
            }

            returnList.Add(savedItem ?? _mapper.Map<Contact>(unSavedItem));
        }

        return returnList;
    }

    private ICollection<Report> AttachExistingReports(ICollection<ReportDto>? unSavedEntities)
    {
        var returnList = new List<Report>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Reports.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

            if (savedItem is not null)
            {
                savedItem.PlannedTasks = unSavedItem.PlannedTasks;
                savedItem.CompletedTasks = unSavedItem.CompletedTasks;
                savedItem.Weeknumber = unSavedItem.Weeknumber;
                savedItem.ProjectId = unSavedItem.ProjectId;
                savedItem.SubmissionDate = unSavedItem.SubmissionDate;
                savedItem.UserId = unSavedItem.UserId;
                savedItem.Risks = AttachExistingRisks(unSavedItem.Risks);
            }

            returnList.Add(savedItem ?? _mapper.Map<Report>(unSavedItem));
        }

        return returnList;
    }

    private ICollection<Risk> AttachExistingRisks(ICollection<RiskDto>? unSavedEntities)
    {
        var returnList = new List<Risk>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Risks.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

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

    private ICollection<Sow> AttachExistingSows(ICollection<SowDto>? unSavedEntities)
    {
        var returnList = new List<Sow>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Sows.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

            if (savedItem is not null)
            {
                savedItem.ProjectId = unSavedItem.ProjectId;
                savedItem.File = unSavedItem.File;
                savedItem.IsChangeRequest = unSavedItem.IsChangeRequest;
                savedItem.SowStartDate = unSavedItem.StartDate;
                savedItem.SowEndDate = unSavedItem.EndDate;
            }

            returnList.Add(savedItem ?? _mapper.Map<Sow>(unSavedItem));
        }

        return returnList;
    }
}


