using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[ExcludeFromCodeCoverage]
[Table("clientprojects")]
public class ClientProject : EntityBase<Guid>, IAggregateRoot
{
    private ClientProject() { }

    public ClientProject(Guid id, Guid clientid, Guid projectid)
    {
        Id = id;
        ClientId = clientid;
        ProjectId = projectid;
    }

    public Guid ClientId { get; set; }
    public Guid ProjectId { get; set; }
#if ADD_ENTITY_NAV
    public virtual Client Client { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
#endif

}