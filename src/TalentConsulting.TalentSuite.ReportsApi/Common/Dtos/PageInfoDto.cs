namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public sealed record PageInfoDto(int TotalCount, int Page, int PageSize, int First, int Last);