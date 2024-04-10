using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record CreateRiskDto(
    string Description,
    string Mitigation,
    string Status
)
{
    public static CreateRiskDto From(Risk risk)
    {
        return new CreateRiskDto(
            risk.Description,
            risk.Mitigation,
            risk.Status.ToString()
        );
    }

    internal Risk ToEntity()
    {
        return new Risk()
        {
            Description = Description,
            Mitigation = Mitigation,
            Status = (RiskStatus)Enum.Parse(typeof(RiskStatus), Status)
        };
    }
}