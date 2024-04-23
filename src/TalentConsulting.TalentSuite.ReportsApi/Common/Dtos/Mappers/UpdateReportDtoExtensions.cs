using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal static class UpdateReportDtoExtensions
{
    public static Report ToEntity(this UpdateReportDto dto)
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