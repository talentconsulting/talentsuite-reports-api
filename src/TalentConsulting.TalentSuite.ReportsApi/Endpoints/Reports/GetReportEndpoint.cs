using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class GetReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapGet("/reports/{id}", GetReport)
            .Produces<ReportDto>(200)
            .Produces(500)
            .WithTags("Reporting")
            .WithDescription("The report to return")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> GetReport(
        [FromServices] IReportsProvider reportsProvider,
        Guid id,
        CancellationToken cancellationToken)
    {
        var report = await reportsProvider.Fetch(id, cancellationToken);
        return report == null
            ? Results.NotFound()
            : TypedResults.Ok(ReportDto.From(report));
    }
}
