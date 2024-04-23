using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
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
    public string? Mitigation { get; set; }
    public RiskStatus Status { get; set; }
}