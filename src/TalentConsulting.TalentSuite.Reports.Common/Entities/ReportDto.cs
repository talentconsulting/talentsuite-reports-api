namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ReportDto
{
    private ReportDto() { }

    public ReportDto(string id, DateTime created, string plannedtasks, string completedtasks, int weeknumber, DateTime submissiondate, int projectid, string userid)
    {
        Id = id;
        Created = created;
        PlannedTasks = plannedtasks;
        CompletedTasks = completedtasks;
        Weeknumber = weeknumber;
        SubmissionDate = submissiondate;
        ProjectId = projectid;
        UserId = userid;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public string PlannedTasks { get; init; } = default!;
    public string CompletedTasks { get; init; } = default!;
    public int Weeknumber { get; init; } = default!;
    public DateTime SubmissionDate { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
    public string UserId { get; init; } = default!;

}