using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

internal sealed class PutReportEndpoint : IApiEndpoint
{
    public static void Register(WebApplication app)
    {
        app.MapPut("/reports", PutReport)
            .Accepts<ReportDto>(false, MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Reporting")
            .WithDescription("The report to update")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> PutReport(
        [FromServices] IReportsProvider reportsProvider,
        [FromServices] IValidator<UpdateReportDto> validator,
        [FromBody] UpdateReportDto reportDto,
        CancellationToken cancellationToken)
    {
        var results = validator.Validate(reportDto);
        if (!results.IsValid)
        {
            return Results.ValidationProblem(results.ToDictionary());
        }

        var report = await reportsProvider.Update(reportDto.ToEntity(), cancellationToken);

        return report is null
            ? Results.NotFound()
            : Results.NoContent();
    }
}
