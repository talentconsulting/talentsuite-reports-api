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
            .Produces< ReportsResponse>(StatusCodes.Status200OK)
            .WithTags("Reporting")
            .WithDescription("Return a paged view of reports")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<ReportsResponse> GetReports(
        [FromServices] IReportsProvider reportsProvider,
        int page,
        int pageSize,
        //Guid userId,
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var paging = new PageQueryParameters(page, pageSize);
        var pagedResults = await reportsProvider.FetchAllBy(projectId, paging, cancellationToken);
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
