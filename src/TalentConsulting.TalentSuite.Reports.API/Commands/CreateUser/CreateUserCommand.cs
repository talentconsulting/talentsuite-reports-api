using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;


public class CreateUserCommand : IRequest<string>, ICreateUserCommand
{
    public CreateUserCommand(UserDto reportDto)
    {
        UserDto = reportDto;
    }

    public UserDto UserDto { get; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    public CreateUserCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var unsavedEntity = _mapper.Map<User>(request.UserDto);
            ArgumentNullException.ThrowIfNull(unsavedEntity);

            var existing = _context.Users.FirstOrDefault(e => unsavedEntity.Id == e.Id);

            if (existing is not null)
                throw new InvalidOperationException($"User with Id: {unsavedEntity.Id} already exists, Please use Update command");

            unsavedEntity.Reports = AttachExistingReports(unsavedEntity.Reports);
#if USE_DISPATCHER
            unsavedEntity.RegisterDomainEvent(new UserCreatedEvent(unsavedEntity));
#endif
            _context.Users.Add(unsavedEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating a project. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.UserDto is not null)
            return request.UserDto.Id;
        else
            return string.Empty;
    }

    private ICollection<Report> AttachExistingReports(ICollection<Report>? unSavedEntities)
    {
        var returnList = new List<Report>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Reports
            .Include(x => x.Risks)
            .Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);

            var item = savedItem ?? unSavedItem;
            item.Risks = AttachExistingRisk(unSavedItem.Risks);
            returnList.Add(item);
        }

        return returnList;
    }

    private ICollection<Risk> AttachExistingRisk(ICollection<Risk>? unSavedEntities)
    {
        var returnList = new List<Risk>();

        if (unSavedEntities is null || !unSavedEntities.Any())
            return returnList;

        var existing = _context.Risks.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

        for (var i = 0; i < unSavedEntities.Count; i++)
        {
            var unSavedItem = unSavedEntities.ElementAt(i);
            var savedItem = existing.FirstOrDefault(x => x.Id == unSavedItem.Id);
            returnList.Add(savedItem ?? unSavedItem);
        }

        return returnList;
    }
}
