using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;


[Table("userprojectroles")]
public class UserProjectRole : EntityBase<string>, IAggregateRoot
{
    private UserProjectRole() { }

    public UserProjectRole(string id, string userid, string projectid, bool recievesreports)
    {
        base.id = id;
        UserId = userid;
        ProjectId = projectid;
        Recievesreports = recievesreports;
    }

    public string UserId { get; init; } = null!;
    public string ProjectId { get; init; } = null!;
    public bool Recievesreports { get; set; }

#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
    public virtual ProjectRole Projectrole { get; set; } = null!;
    public virtual User User { get; set; } = null!;
#endif
}