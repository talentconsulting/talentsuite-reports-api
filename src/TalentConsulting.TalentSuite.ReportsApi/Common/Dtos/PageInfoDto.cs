namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

internal sealed record PageInfoDto(int TotalCount, int Page, int PageSize, int First, int Last);