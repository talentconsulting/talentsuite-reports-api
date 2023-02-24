namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ClientProjectDto
{
    private ClientProjectDto() { }

    public ClientProjectDto(string id, string clientId, string projectId)
    {
        Id = id;
        ClientId = clientId;
        ProjectId = projectId;
    }

    public string Id { get; init; } = default!;
    public string ClientId { get; init; } = default!;
    public string ProjectId { get; init; } = default!;
#if ADD_ENTITY_NAV
    public virtual ClientDto Client { get; set; } = default!;
    public virtual ProjectDto Project { get; set; } = default!;
#endif
}