namespace TalentConsulting.TalentSuite.ReportsApi.Common;

public record PageQueryParameters(int Page, int PageSize)
{
    public int SafePage => Math.Clamp(Page, 1, 999);
    public int SafePageSize => Math.Clamp(PageSize, 1, 100);
}
