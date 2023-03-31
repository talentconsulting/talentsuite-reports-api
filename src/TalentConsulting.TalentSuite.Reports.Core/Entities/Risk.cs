using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("risks")]
public class Risk : EntityBase<string>, IAggregateRoot
{
    private Risk() { }

    public Risk(string id, string reportid, string riskdetails, string riskmitigation, string ragstatus)
    {
        base.id = id;
        ReportId = reportid;
        RiskDetails = riskdetails;
        RiskMitigation = riskmitigation;
        RagStatus = ragstatus;
    }

    public string ReportId { get; set; } = default!;
    public string RiskDetails { get; set; } = default!;
    public string RiskMitigation { get; set; } = default!;
    public string RagStatus { get; set; } = default!;
#if ADD_ENTITY_NAV
    public virtual Report Report { get; set; } = null!;
#endif

}