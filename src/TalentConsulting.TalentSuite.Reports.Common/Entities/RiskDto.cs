namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record RiskDto
{
    private RiskDto() { }

    public RiskDto(string id, int reportid, string riskdetails, string riskmitigation, int ragstatus)
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
    public string RagStatus { get; init; } = default!;
}