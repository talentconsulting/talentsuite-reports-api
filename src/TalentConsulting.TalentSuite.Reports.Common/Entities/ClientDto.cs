namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ClientDto
{
    private ClientDto() { }

    public ClientDto(string id, string fullname, string contactname, string contactemail)
    {
        Id = id;
        FullName = fullname;
        ContactName = contactname;
        ContactEmail = contactemail;
    }

    public string Id { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string ContactName { get; init; } = default!;
    public string ContactEmail { get; init; } = default!;
}