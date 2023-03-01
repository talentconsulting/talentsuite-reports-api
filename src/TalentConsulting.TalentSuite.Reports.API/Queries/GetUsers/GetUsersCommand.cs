using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetUsers;

public class GetUsersCommand : IRequest<PaginatedList<UserDto>>
{
    public GetUsersCommand(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber != null ? pageNumber.Value : 1;
        PageSize = pageSize != null ? pageSize.Value : 1;
    }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, PaginatedList<UserDto>>
{
    private readonly ApplicationDbContext _context;

    public GetUsersCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<UserDto>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Users
            .Include(x => x.Reports)
            .ThenInclude(x => x.Risks);

        if (entities == null)
        {
            throw new NotFoundException(nameof(User), "Users");
        }

        var filteredUsers = await entities.Select(x => new UserDto(x.Id, x.Firstname, x.Lastname, x.Email, x.UserGroupId,
            EntityToDtoHelper.GetReports(x.Reports)
            )).ToListAsync();

        if (request != null)
        {
            var pagelist = filteredUsers.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            var result = new PaginatedList<UserDto>(filteredUsers, pagelist.Count, request.PageNumber, request.PageSize);
            return result;
        }

        return new PaginatedList<UserDto>(filteredUsers, filteredUsers.Count, 1, 10);
    }
}

