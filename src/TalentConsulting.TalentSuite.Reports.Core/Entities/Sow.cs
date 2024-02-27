using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("sows")]
public class Sow : EntityBase<Guid>, IAggregateRoot
{
    private Sow() { }

    public Sow(string id, DateTime created, byte[] file, bool ischangerequest, DateTime sowstartdate, DateTime sowenddate, string projectid)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        Created = created;
        File = file;
        IsChangeRequest = ischangerequest;
        SowStartDate = sowstartdate;
        SowEndDate = sowenddate;
        ProjectId = projectid;
    }

    public byte[] File { get; set; } = null!;
    public bool IsChangeRequest { get; set; }
    public DateTime SowStartDate { get; set; }
    public DateTime SowEndDate { get; set; }
    public string ProjectId { get; set; } = null!;
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

}
