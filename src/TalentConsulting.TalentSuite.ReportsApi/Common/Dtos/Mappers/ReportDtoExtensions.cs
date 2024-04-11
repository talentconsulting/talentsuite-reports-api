using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class ReportDtoExtensions
{
    public static ReportDto ToReportDto(this Report report)
    {
        return new ReportDto(
            report.Id,
            report.ClientId, report.ProjectId, report.SowId,
            report.Completed,
            report.Planned,
            report.Risks.Select(risk => risk.ToRiskDto()),
            report.Status
        );
    }

    public static Report ToEntity(this ReportDto dto)
    {
        return new Report()
        {
            Id = dto.Id,
            ClientId = dto.ClientId,
            ProjectId = dto.ProjectId,
            SowId = dto.SowId,
            Completed = dto.Completed,
            Planned = dto.Planned,
            Risks = dto.Risks.Select(x => x.ToEntity()).ToList(),
            Status = dto.Status
        };
    }
}