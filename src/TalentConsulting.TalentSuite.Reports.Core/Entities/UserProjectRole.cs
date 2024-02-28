using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;


[Table("userprojectroles")]
public class UserProjectRole : EntityBase<Guid>, IAggregateRoot
{
    private UserProjectRole() { }

    public UserProjectRole(Guid id, Guid userid, Guid projectid, bool recievesreports)
    {
        Id = id;
        UserId = userid;
        ProjectId = projectid;
        Recievesreports = recievesreports;
    }

    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public bool Recievesreports { get; set; }

#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
    public virtual ProjectRole Projectrole { get; set; } = null!;
    public virtual User User { get; set; } = null!;
#endif
}