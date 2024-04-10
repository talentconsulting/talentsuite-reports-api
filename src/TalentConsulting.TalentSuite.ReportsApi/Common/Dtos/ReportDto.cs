using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record ReportDto(
    Guid Id,
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IEnumerable<RiskDto> Risks,
    string Status
)
{
    public static ReportDto From(Report report)
    {
        return new ReportDto(
            report.Id,
            report.ClientId, report.ProjectId, report.SowId,
            report.Completed,
            report.Planned,
            report.Risks.Select(RiskDto.From),
            report.Status.ToString()
        );
    }

    internal Report ToEntity()
    {
        return new Report()
        {
            Id = Id,
            ClientId = ClientId,
            ProjectId = ProjectId,
            SowId = SowId,
            Completed = Completed,
            Planned = Planned,
            Risks = Risks.Select(x => x.ToEntity()).ToList(),
            Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), Status)
        };
    }
}