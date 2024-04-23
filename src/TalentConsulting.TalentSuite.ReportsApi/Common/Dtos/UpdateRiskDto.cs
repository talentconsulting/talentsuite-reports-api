using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record struct UpdateRiskDto(Guid? Id, string Description, string? Mitigation, RiskStatus Status);