using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class PutReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapPut("/reports/{id}", PutReport)
            .Produces(500)
            .WithTags("Reporting")
            .WithDescription("The report to update")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> PutReport(
        [FromServices] IReportsProvider reportsProvider,
        //Guid id, // TODO: uncomment this line
        [FromBody] ReportDto reportDto,
        CancellationToken cancellationToken)
    {
        var report = reportDto.ToEntity();
        await reportsProvider.Update(report, cancellationToken);

        return Results.Ok();
    }
}
