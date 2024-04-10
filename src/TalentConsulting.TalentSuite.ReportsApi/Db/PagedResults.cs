namespace TalentConsulting.TalentSuite.ReportsApi.Db;

internal class PagedResults<T>(int start, int total, List<T> results)
{
    public int Start { get; set; } = start;
    public int Total { get; set; } = total;
    public List<T> Results { get; init; } = results;
}
