using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("sows")]
public class Sow : EntityBaseEx<Guid>, IAggregateRoot
{
    private Sow() { }

    public Sow(Guid id, DateTime created, ICollection<SowFile> files, bool ischangerequest, DateTime sowstartdate, DateTime sowenddate, Guid projectid)
    {
        Id = id;
        Created = created;
        IsChangeRequest = ischangerequest;
        SowStartDate = sowstartdate;
        SowEndDate = sowenddate;
        ProjectId = projectid;
        Files = files;
    }

    public bool IsChangeRequest { get; set; }
    public DateTime SowStartDate { get; set; }
    public DateTime SowEndDate { get; set; }
    public Guid ProjectId { get; set; }
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

    public virtual ICollection<SowFile> Files { get; set; } = new Collection<SowFile>();

}
