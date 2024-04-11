using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal sealed record RiskDto(
    Guid Id,
    string Description,
    string Mitigation,
    RiskStatus Status
);