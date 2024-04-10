using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class PutReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapPut("/reports/{id:guid}", PutReport)
            .Accepts<ReportDto>(false, MediaTypeNames.Application.Json)
            .Produces<ReportDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Reporting")
            .WithDescription("The report to update")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> PutReport(
        [FromServices] IReportsProvider reportsProvider,
        Guid id,
        [FromBody] ReportDto reportDto,
        CancellationToken cancellationToken)
    {
        // TODO: validation
        if (reportDto.Id != id)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Detail = "Ids do not match"
            });
        }

        var report = await reportsProvider.Update(reportDto.ToEntity(), cancellationToken);

        return report is null
            ? Results.NotFound()
            : TypedResults.Ok(ReportDto.From(report));
    }
}
