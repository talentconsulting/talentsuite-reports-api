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
            var entity = _mapper.Map<Project>(request.ProjectDto);
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            entity.RegisterDomainEvent(new ProjectCreatedEvent(entity));
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating a project. {exceptionMessage}", ex.Message);
            throw new Exception(ex.Message, ex);
        }

        if (request is not null && request.ProjectDto is not null)
            return request.ProjectDto.Id;
        else
            return string.Empty;
    }
}


