using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Report : EntityBase<string>, IAggregateRoot
{
    private Report() { }

    public Report(string id, string plannedtasks, string completedtasks, int weeknumber, DateTime submissiondate, string projectid, string userid, ICollection<Risk> risks)
    {
        Id = id;
        PlannedTasks = plannedtasks;
        CompletedTasks = completedtasks;
        Weeknumber = weeknumber;
        SubmissionDate = submissiondate;
        ProjectId = projectid;
        UserId = userid;
        Risks = risks;
    }

    public string PlannedTasks { get; init; } = default!;
    public string CompletedTasks { get; init; } = default!;
    public int Weeknumber { get; init; } = default!;
    public DateTime SubmissionDate { get; init; } = default!;
    public string ProjectId { get; init; } = default!;
    public string UserId { get; init; } = default!;    
    public virtual ICollection<Risk> Risks { get; } = new List<Risk>();

#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
    public virtual User User { get; set; } = null!;
#endif

}