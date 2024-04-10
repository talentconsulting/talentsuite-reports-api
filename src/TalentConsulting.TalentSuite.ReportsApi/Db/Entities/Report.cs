using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

public enum ReportStatus
{
    Saved,
    Submitted,
    Retracted,
}

[Table("reports")]
public class Report
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid SowId { get; set; }
    public string? Completed { get; set; }
    public string? Planned { get; set; }
    public ICollection<Risk> Risks { get; set; } = new Collection<Risk>();
    public ReportStatus Status { get; set; }
}