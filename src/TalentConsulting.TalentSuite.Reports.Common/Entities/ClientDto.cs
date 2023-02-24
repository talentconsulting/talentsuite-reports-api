namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ClientDto
{
    private ClientDto() { }

    public ClientDto(string id, string name, string contactname, string contactemail, ICollection<ClientProjectDto> clientProjects)
    {
        Id = id;
        Name = name;
        ContactName = contactname;
        ContactEmail = contactemail;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string ContactName { get; init; } = default!;
    public string ContactEmail { get; init; } = default!;
    public ICollection<ClientProjectDto> ClientProjects { get; } = new List<ClientProjectDto>();
}