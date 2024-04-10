using System.ComponentModel.DataAnnotations.Schema;

namespace TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

public enum RiskStatus
{
    Red,
    Amber,
    Green
}

[Table("risks")]
public class Risk
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Mitigation { get; set; } = string.Empty;
    public RiskStatus Status { get; set; }
}