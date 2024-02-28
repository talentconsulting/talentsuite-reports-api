using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("clientprojects")]
public class ClientProject : EntityBase<Guid>, IAggregateRoot
{
    private ClientProject() { }

    public ClientProject(Guid id, string clientid, string projectid)
    {
        Id = id;
        ClientId = clientid;
        ProjectId = projectid;
    }

    public string ClientId { get; set; } = null!;
    public string ProjectId { get; set; } = null!;
#if ADD_ENTITY_NAV
    public virtual Client Client { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
#endif

}