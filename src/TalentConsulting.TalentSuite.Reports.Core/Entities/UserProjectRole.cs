using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class UserProjectRole : EntityBase<string>, IAggregateRoot
{
    private UserProjectRole() { }

    public UserProjectRole(string id, string userid, string projectid)
    {
        Id = id;
        UserId = userid;
        ProjectId = projectid;
    }

    public string UserId { get; init; } = default!;
    public string ProjectId { get; init; } = default!;

}