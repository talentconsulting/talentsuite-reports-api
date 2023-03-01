using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateUser;


public class UpdateUserCommand : IRequest<string>, IUpdateUserCommand
{
    public UpdateUserCommand(string id, UserDto userDto)
    {
        Id = id;
        UserDto = userDto;
    }

    public string Id { get; }
    public UserDto UserDto { get; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    public UpdateUserCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Users.FirstOrDefault(x => x.Id == request.Id);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Report), request.Id);
        }

        try
        {
            entity.Firstname = request.UserDto.Firstname;
            entity.Lastname = request.UserDto.Lastname;
            entity.Email = request.UserDto.Email;
            entity.UserGroupId = request.UserDto.UserGroupId;
            entity.Reports = AttachExistingReports(request.UserDto.Reports);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating User. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.UserDto is not null)
            return request.UserDto.Id;
        else
            return string.Empty;
    }

    private ICollection<Report> AttachExistingReports(ICollection<ReportDto>? unSavedEntities)
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

            if (savedItem is not null)
            {
                savedItem.PlannedTasks = unSavedItem.PlannedTasks;
                savedItem.CompletedTasks = unSavedItem.CompletedTasks;
                savedItem.Weeknumber = unSavedItem.Weeknumber;
                savedItem.SubmissionDate = unSavedItem.SubmissionDate;
                savedItem.ProjectId = unSavedItem.ProjectId;
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
}


