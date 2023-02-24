using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Risk : EntityBase<string>, IAggregateRoot
{
    private Risk() { }

    public Risk(string id, string reportid, string riskdetails, string riskmitigation, string ragstatus)
    {
        Id = id;
        ReportId = reportid;
        RiskDetails = riskdetails;
        RiskMitigation = riskmitigation;
        RagStatus = ragstatus;
    }

    public string ReportId { get; init; } = default!;
    public string RiskDetails { get; init; } = default!;
    public string RiskMitigation { get; init; } = default!;
    public string RagStatus { get; init; } = default!;
#if ADD_ENTITY_NAV
    public virtual Report Report { get; set; } = null!;
#endif

}