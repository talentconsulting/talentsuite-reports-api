using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;

public class GetProjectByIdCommand : IRequest<ProjectDto>
{
    public GetProjectByIdCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

}

public class GetProjectByIdCommandHandler : IRequestHandler<GetProjectByIdCommand, ProjectDto>
{
    private readonly ApplicationDbContext _context;

    public GetProjectByIdCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ProjectDto> Handle(GetProjectByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .Include(x => x.ClientProjects)
            .Include(x => x.Contacts)
            .Include(x => x.Reports)
            .ThenInclude(x => x.Risks)
            .Include(x => x.Sows)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        var result = EntityToDtoHelper.ProjectDtoToProjectDto(entity);

        return result;
    }
}

