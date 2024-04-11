using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal record ReportDto(
    Guid Id,
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IEnumerable<RiskDto> Risks,
    ReportStatus Status
);