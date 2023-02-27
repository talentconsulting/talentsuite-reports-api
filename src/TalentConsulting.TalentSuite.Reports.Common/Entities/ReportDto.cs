namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ReportDto
{
    private ReportDto() { }

    public ReportDto(string id, DateTime created, string plannedtasks, string completedtasks, int weeknumber, DateTime submissiondate, string projectId, string userId, ICollection<RiskDto> risks)
    {
        Id = id;
        Created = created;
        PlannedTasks = plannedtasks;
        CompletedTasks = completedtasks;
        Weeknumber = weeknumber;
        SubmissionDate = submissiondate;
        ProjectId = projectId;
        UserId = userId;
        Risks = risks;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public string PlannedTasks { get; init; } = default!;
    public string CompletedTasks { get; init; } = default!;
    public int Weeknumber { get; init; } = default!;
    public DateTime SubmissionDate { get; init; } = default!;
    public string ProjectId { get; init; } = default!;
    //public ProjectDto Project { get; set; } = null!;
    public string UserId { get; init; } = default!;
    //public UserDto User { get; set; } = null!;
    public virtual ICollection<RiskDto> Risks { get; init; } = default!;
#if ADD_ENTITY_NAV
    
#endif

}