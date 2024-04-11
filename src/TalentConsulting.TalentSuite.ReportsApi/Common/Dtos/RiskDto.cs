using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal record RiskDto(
    Guid Id,
    string Description,
    string Mitigation,
    RiskStatus Status
)
{
    public static RiskDto From(Risk risk)
    {
        return new RiskDto(
            risk.Id,
            risk.Description,
            risk.Mitigation,
            risk.Status
        );
    }

    internal Risk ToEntity()
    {
        return new Risk()
        {
            Id = Id,
            Description = Description,
            Mitigation = Mitigation,
            Status = Status
        };
    }
}