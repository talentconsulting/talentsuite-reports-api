using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public record CreateReportDto(
    Guid ClientId,
    Guid ProjectId,
    Guid SowId,
    string? Completed,
    string? Planned,
    IEnumerable<CreateRiskDto> Risks,
    ReportStatus Status
)
{
    public static CreateReportDto From(Report report)
    {
        return new CreateReportDto(
            report.ClientId,
            report.ProjectId,
            report.SowId,
            report.Completed,
            report.Planned,
            report.Risks.Select(CreateRiskDto.From),
            report.Status
        );
    }

    internal Report ToEntity()
    {
        return new Report()
        {
            ClientId = ClientId,
            ProjectId = ProjectId,
            SowId = SowId,
            Completed = Completed,
            Planned = Planned,
            Risks = Risks.Select(x => x.ToEntity()).ToList(),
            //Status = (ReportStatus)Enum.Parse(typeof(ReportStatus), Status)
            Status = Status
        };
    }
}