using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record struct UpdateReportDto(
    Guid Id,
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IReadOnlyCollection<UpdateRiskDto> Risks,
    ReportStatus Status
);