using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal record RiskDto(
    Guid Id,
    string Description,
    string Mitigation,
    RiskStatus Status
);