namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ContactDto
{
    private ContactDto() { }

    public ContactDto(string id, string firstname, string email, bool receivesreport, string projectId)
    {
        Id = id;
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = projectId;
    }

    public string Id { get; init; } = default!;
    public string Firstname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReport { get; init; } = default!;
    public string ProjectId { get; init; } = default!;
#if ADD_ENTITY_NAV
    public ProjectDto Project { get; set; } = null!;
#endif
    
}

