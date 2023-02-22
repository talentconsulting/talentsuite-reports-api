using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class ClientProject : EntityBase<string>, IAggregateRoot
{
    private ClientProject() { }

    public ClientProject(string id, int clientid, int projectid, string contactemail)
    {
        Id = id;
        ClientId = clientid;
        ProjectId = projectid;
    }

    public int ClientId { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
}