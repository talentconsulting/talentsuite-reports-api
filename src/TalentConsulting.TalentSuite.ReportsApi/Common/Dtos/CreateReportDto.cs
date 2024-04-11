using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal record CreateReportDto(
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IEnumerable<CreateRiskDto> Risks,
    ReportStatus Status
);