using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class UpdateRiskDtoExtensions
{
    public static Risk ToEntity(this UpdateRiskDto dto)
    {
        var risk = new Risk()
        {
            Description = dto.Description,
            Mitigation = dto.Mitigation,
            Status = dto.Status
        };

        if (dto.Id.HasValue)
        {
            risk.Id = dto.Id.Value;
        }

        return risk;
    }
}