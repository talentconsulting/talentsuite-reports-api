using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;


[Table("userprojectroles")]
public class UserProjectRole : IAggregateRoot
{
    private UserProjectRole() { }

    public UserProjectRole(string id, string userid, string projectid, bool recievesreports)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        if (Guid.TryParse(userid, out Guid guidUserId))
        {
            UserId = guidUserId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(userid));
        }
        if (Guid.TryParse(projectid, out Guid guidProjectId))
        {
            ProjectId = guidProjectId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(projectid));
        }
        Recievesreports = recievesreports;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public bool Recievesreports { get; set; }

#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
    public virtual ProjectRole Projectrole { get; set; } = null!;
    public virtual User User { get; set; } = null!;
#endif
}