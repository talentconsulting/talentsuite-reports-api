using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Requests;

public sealed class GetReportsEndpoint : IApiEndpoint
{
    internal record ReportsResponse(PageInfoDto PageInfo, IEnumerable<ReportDto> Reports);

    public static void Register(WebApplication app)
    {
        app.MapGet("/reports", GetReports)
            .Produces(200)
            .WithTags("Reporting")
            .WithDescription("Return a paged view of reports in normalised form")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<ReportsResponse> GetReports(int page, int pageSize, /*Guid userId, Guid projectId,*/ [FromServices] IReportsProvider reportsProvider)
    {
        var projectId = new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f");
        var paging = new PageQueryParameters(page, pageSize);
        var pagedResults = await reportsProvider.GetAllBy(projectId, paging);
        var mappedResults = pagedResults.Results.Select(report => ReportDto.From(report));
        var pagingInfo = new PageInfoDto(
            pagedResults.Total,
            paging.SafePage,
            paging.SafePageSize,
            pagedResults.Start,
            Math.Max(pagedResults.Start + pagedResults.Results.Count - 1, 0));

        return new ReportsResponse(pagingInfo, mappedResults);
    }
}
