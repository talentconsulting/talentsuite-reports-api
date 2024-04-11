using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

internal sealed class GetReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapGet("/reports/{id:guid}", GetReport)
            .Produces<ReportDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Reporting")
            .WithDescription("The report to return")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> GetReport(
        [FromServices] IReportsProvider reportsProvider,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var report = await reportsProvider.Fetch(id, cancellationToken);
        return report == null
            ? Results.NotFound()
            : TypedResults.Ok(report.ToReportDto());
    }
}
