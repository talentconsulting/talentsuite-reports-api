using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class RiskDtoExtensions
{
    public static RiskDto ToRiskDto(this Risk risk)
    {
        return new RiskDto(
            risk.Id,
            risk.Description,
            risk.Mitigation,
            risk.Status
        );
    }

    public static Risk ToEntity(this RiskDto dto)
    {
        return new Risk()
        {
            Id = dto.Id,
            Description = dto.Description,
            Mitigation = dto.Mitigation,
            Status = dto.Status
        };
    }
}