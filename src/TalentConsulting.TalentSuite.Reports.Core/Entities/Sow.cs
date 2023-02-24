using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Sow : EntityBase<string>, IAggregateRoot
{
    private Sow() { }

    public Sow(string id, DateTime created, byte[] file, bool ischangerequest, DateTime sowstartdate, DateTime sowenddate, string projectid)
    {
        Id = id;
        Created = created;
        File = file;
        IsChangeRequest = ischangerequest;
        SowStartDate = sowstartdate;
        SowEndDate = sowenddate;
        ProjectId = projectid;
    }

    public byte[] File { get; init; } = null!;
    public bool IsChangeRequest { get; init; }
    public DateTime SowStartDate { get; init; }
    public DateTime SowEndDate { get; init; }
    public string ProjectId { get; init; } = null!;
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

}
