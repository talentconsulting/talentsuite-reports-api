using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public sealed record ReportDto(
    Guid Id,
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IReadOnlyCollection<RiskDto> Risks,
    ReportStatus Status
);