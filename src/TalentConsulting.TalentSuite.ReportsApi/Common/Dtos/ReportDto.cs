using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public sealed record ReportDto(
    Guid Id,
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IReadOnlyCollection<RiskDto> Risks,
    ReportStatus Status
)
{
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(ReportDto? other)
    {
        return other is not null
            && Risks.Count == other.Risks.Count
            && Id == other.Id
            && ClientId == other.ClientId
            && ProjectId == other.ProjectId
            && SowId == other.SowId
            && Completed == other.Completed
            && Planned == other.Planned
            && Status == other.Status
            && Risks.All(r => r == other.Risks.FirstOrDefault());
    }
};