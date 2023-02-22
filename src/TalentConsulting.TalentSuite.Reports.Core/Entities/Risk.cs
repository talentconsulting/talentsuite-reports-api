using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Risk : EntityBase<string>, IAggregateRoot
{
    private Risk() { }

    public Risk(string id, int reportid, string riskdetails, string riskmitigation, int ragstatus)
    {
        Id = id;
        ReportId = reportid;
        RiskDetails = riskdetails;
        RiskMitigation = riskmitigation;
        RagStatus = ragstatus;
    }

    public string Id { get; init; } = default!;
    public int ReportId { get; init; } = default!;
    public string RiskDetails { get; init; } = default!;
    public string RiskMitigation { get; init; } = default!;
    public int RagStatus { get; init; } = default!;
}