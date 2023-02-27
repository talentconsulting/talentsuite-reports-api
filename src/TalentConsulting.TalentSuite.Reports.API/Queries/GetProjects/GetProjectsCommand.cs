using Ardalis.GuardClauses;
using Ardalis.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;

public class GetProjectsCommand : IRequest<PaginatedList<ProjectDto>>
{
    public GetProjectsCommand(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber != null ? pageNumber.Value : 1;
        PageSize = pageSize != null ? pageSize.Value : 1;
    }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProjectsCommandHandler : IRequestHandler<GetProjectsCommand, PaginatedList<ProjectDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProjectsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<ProjectDto>> Handle(GetProjectsCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Projects
            .Include(x => x.ClientProjects)
            .Include(x => x.Contacts)
            .Include(x => x.Reports)
            .ThenInclude(x => x.Risks)
            .Include(x => x.Sows);

        if (entities == null)
        {
            throw new NotFoundException(nameof(Project), "Projects");
        }

        var filteredProjects = await entities.Select(x => EntityToDtoHelper.ProjectDtoToProjectDto(x)).ToListAsync();

        if (request != null)
        {
            var pagelist = filteredProjects.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            var result = new PaginatedList<ProjectDto>(filteredProjects, pagelist.Count, request.PageNumber, request.PageSize);
            return result;
        }

        return new PaginatedList<ProjectDto>(filteredProjects, filteredProjects.Count, 1, 10);


    }
}

