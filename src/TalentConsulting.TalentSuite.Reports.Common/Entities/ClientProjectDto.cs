namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ClientProjectDto
{
    private ClientProjectDto() { }

    public ClientProjectDto(string id, int clientid, int projectid, string contactemail)
    {
        Id = id;
        ClientId = clientid;
        ProjectId = projectid;
    }

    public string Id { get; init; } = default!;
    public int ClientId { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
}