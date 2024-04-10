using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class PostReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapPost("/reports", PostReport)
            .Produces<ReportDto>(200)
            .Produces(500)
            .WithTags("Reporting")
            .WithDescription("The report to create")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> PostReport(
        [FromServices] IReportsProvider reportsProvider,
        [FromBody] CreateReportDto createReportDto,
        CancellationToken cancellationToken)
    {
        var report = await reportsProvider.Create(createReportDto.ToEntity(), cancellationToken);
        return TypedResults.Ok(ReportDto.From(report));
    }
}
