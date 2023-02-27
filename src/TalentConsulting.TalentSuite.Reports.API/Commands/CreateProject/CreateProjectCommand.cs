using AutoMapper;
using MediatR;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Events;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;

public class CreateProjectCommand : IRequest<string>, ICreateProjectCommand
{
    public CreateProjectCommand(ProjectDto projectDto)
    {
        ProjectDto = projectDto;
    }

    public ProjectDto ProjectDto { get; }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProjectCommandHandler> _logger;
    public CreateProjectCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateProjectCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var unsavedEntity = _mapper.Map<Project>(request.ProjectDto);
            ArgumentNullException.ThrowIfNull(unsavedEntity);

            var existing = _context.Projects.FirstOrDefault(e => unsavedEntity.Id == e.Id);

            if (existing is not null)
                throw new InvalidOperationException($"Project with Id: {unsavedEntity.Id} already exists, Please use Update command");

            unsavedEntity.ClientProjects = AttachExistingClientProjects(unsavedEntity.ClientProjects);
            unsavedEntity.Contacts = AttachExistingContacts(unsavedEntity.Contacts);
            unsavedEntity.Reports= AttachExistingReports(unsavedEntity.Reports);
            unsavedEntity.Sows = AttachExistingSows(unsavedEntity.Sows);

            unsavedEntity.RegisterDomainEvent(new ProjectCreatedEvent(unsavedEntity));
            _context.Projects.Add(unsavedEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating a project. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.ProjectDto is not null)
            return request.ProjectDto.Id;
        else
            return string.Empty;
    }

    private ICollection<ClientProject> AttachExistingClientProjects(ICollection<ClientProject>? unSavedEntities)
    {
        var returnList = new List<ClientProject>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.ClientProjects.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }

    private ICollection<Contact> AttachExistingContacts(ICollection<Contact>? unSavedEntities)
    {
        var returnList = new List<Contact>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Contacts.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }

    private ICollection<Report> AttachExistingReports(ICollection<Report>? unSavedEntities)
    {
        var returnList = new List<Report>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Reports.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }

    private ICollection<Sow> AttachExistingSows(ICollection<Sow>? unSavedEntities)
    {
        var returnList = new List<Sow>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Sows.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }
}


