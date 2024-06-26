﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
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
    public ICollection<Risk> Risks { get; set; } = [];
    public ReportStatus Status { get; set; }
}