using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<string>, IUpdateClientCommand
{
    public UpdateClientCommand(string id, ClientDto reportDto)
    {
        Id = id;
        ClientDto = reportDto;
    }

    public string Id { get; }
    public ClientDto ClientDto { get; }
}

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateClientCommandHandler> _logger;
    public UpdateClientCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateClientCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Clients.FirstOrDefault(x => x.Id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

        try
        {
            entity.Name = request.ClientDto.Name;
            entity.ContactName = request.ClientDto.ContactName;
            entity.ContactEmail = request.ClientDto.ContactEmail;

            entity.ClientProjects = AttachExistingClientProjects(request.ClientDto.ClientProjects);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating Client. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.ClientDto is not null)
            return request.ClientDto.Id;
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
                savedItem.ProjectId = unSavedItem.ProjectId;
            }

            returnList.Add(savedItem ?? _mapper.Map<ClientProject>(unSavedItem));
        }

        return returnList;
    }
}
