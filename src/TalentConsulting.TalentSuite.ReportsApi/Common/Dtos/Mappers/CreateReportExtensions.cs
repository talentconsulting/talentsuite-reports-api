using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class CreateReportExtensions
{
    public static Report ToEntity(this CreateReportDto dto)
    {
        return new Report()
        {
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