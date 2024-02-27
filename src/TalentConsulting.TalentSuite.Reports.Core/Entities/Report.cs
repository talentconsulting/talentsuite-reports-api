using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;


[Table("reports")]
public class Report : EntityBase<Guid>, IAggregateRoot
{
    private Report() { }

    public Report(string id, string plannedtasks, string completedtasks, int weeknumber, DateTime submissiondate, string projectid, string userid, ICollection<Risk> risks)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        PlannedTasks = plannedtasks;
        CompletedTasks = completedtasks;
        Weeknumber = weeknumber;
        SubmissionDate = submissiondate;
        if (Guid.TryParse(projectid, out Guid guidProjectId))
        {
            ProjectId = guidProjectId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(projectid));
        }
        if (Guid.TryParse(userid, out Guid guidUserId))
        {
            UserId = guidUserId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(userid));
        }
        Risks = risks;
    }

    public string PlannedTasks { get; set; } = default!;
    public string CompletedTasks { get; set; } = default!;
    public int Weeknumber { get; set; } = default!;
    public DateTime SubmissionDate { get; set; } = default!;
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public virtual ICollection<Risk> Risks { get; set;  } = new Collection<Risk>();

#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
    public virtual User User { get; set; } = null!;
#endif

}