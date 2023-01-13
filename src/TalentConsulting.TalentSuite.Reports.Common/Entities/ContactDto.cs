namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ContactDto
{
    private ContactDto() { }

    public ContactDto(string id, string firstname, string email, bool receivesreport, int startdate)
    {
        Id = id;
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = startdate;
    }

    public string Id { get; init; } = default!;
    public string Firstname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReport { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
}

