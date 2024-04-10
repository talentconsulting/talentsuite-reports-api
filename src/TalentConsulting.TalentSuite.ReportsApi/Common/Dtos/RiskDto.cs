using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record RiskDto(
    Guid Id,
    string Description,
    string Mitigation,
    string Status
)
{
    public static RiskDto From(Risk risk)
    {
        return new RiskDto(
            risk.Id,
            risk.Description,
            risk.Mitigation,
            risk.Status.ToString()
        );
    }

    internal Risk ToEntity()
    {
        return new Risk()
        {
            Id = Id,
            Description = Description,
            Mitigation = Mitigation,
            Status = (RiskStatus)Enum.Parse(typeof(RiskStatus), Status)
        };
    }
}