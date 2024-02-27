using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("risks")]
public class Risk : IAggregateRoot
{
    private Risk() { }

    public Risk(string id, string reportid, string riskdetails, string riskmitigation, string ragstatus)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        if (Guid.TryParse(reportid, out Guid guidReportId))
        {
            ReportId = guidReportId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(reportid));
        }
        RiskDetails = riskdetails;
        RiskMitigation = riskmitigation;
        RagStatus = ragstatus;
    }

    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string RiskDetails { get; set; } = default!;
    public string RiskMitigation { get; set; } = default!;
    public string RagStatus { get; set; } = default!;
#if ADD_ENTITY_NAV
    public virtual Report Report { get; set; } = null!;
#endif

}