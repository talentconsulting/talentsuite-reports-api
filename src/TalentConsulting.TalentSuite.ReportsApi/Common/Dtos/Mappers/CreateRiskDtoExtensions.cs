using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class CreateRiskDtoExtensions
{
    public static Risk ToEntity(this CreateRiskDto dto)
    {
        return new Risk()
        {
            Description = dto.Description,
            Mitigation = dto.Mitigation,
            Status = dto.Status
        };
    }
}