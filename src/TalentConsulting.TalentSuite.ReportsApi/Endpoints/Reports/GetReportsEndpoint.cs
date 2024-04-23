using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Requests;

internal sealed class GetReportsEndpoint : IApiEndpoint
{
    internal record ReportsResponse(PageInfoDto PageInfo, IEnumerable<ReportDto> Reports);

    public static void Register(WebApplication app)
    {
        app.MapGet("/reports", GetReports)
            .Produces<ReportsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Reporting")
            .WithDescription("Return a paged view of reports")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> GetReports(
        [FromServices] IReportsProvider reportsProvider,
        int page,
        int pageSize,
        //Guid userId,
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var paging = new PageQueryParameters(page, pageSize);
        var pagedResults = await reportsProvider.FetchAllBy(projectId, paging, cancellationToken);
        var mappedResults = pagedResults.Results.Select(report => report.ToReportDto());

        return TypedResults.Ok(new ReportsResponse(pagedResults.ToPageInfoDto(), mappedResults));
    }
}
