using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("clientprojects")]
public class ClientProject : IAggregateRoot
{
    private ClientProject() { }

    public ClientProject(string id, string clientid, string projectid)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        ClientId = clientid;
        ProjectId = projectid;
    }

    public Guid Id { get; set; }
    public string ClientId { get; set; } = null!;
    public string ProjectId { get; set; } = null!;
#if ADD_ENTITY_NAV
    public virtual Client Client { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
#endif

}