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
            entity.ClId = request.ProjectDto.ClId;
            entity.Name = request.ProjectDto.Name;
            entity.Reference = request.ProjectDto.Reference;
            entity.StartDate = request.ProjectDto.StartDate;
            entity.EndDate = request.ProjectDto.EndDate;
            
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating project. {exceptionMessage}", ex.Message);
            throw new Exception(ex.Message, ex);
        }

        if (request is not null && request.ProjectDto is not null)
            return request.ProjectDto.Id;
        else
            return string.Empty;
    }
}


