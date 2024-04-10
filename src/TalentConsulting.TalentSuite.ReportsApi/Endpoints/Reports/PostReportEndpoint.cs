using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class PostReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapPost("/reports", PostReport)
            .Accepts<CreateReportDto>(false, MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Reporting")
            .WithDescription("The report to create")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> PostReport(
        HttpContext http,
        [FromServices] IReportsProvider reportsProvider,
        [FromServices] IValidator<CreateReportDto> validator,
        [FromBody] CreateReportDto createReportDto,
        CancellationToken cancellationToken)
    {
        var results = validator.Validate(createReportDto);
        if (!results.IsValid)
        {
            return Results.ValidationProblem(results.ToDictionary());
        }

        var report = await reportsProvider.Create(createReportDto.ToEntity(), cancellationToken);
        http.Response.Headers.Location = $"/reports/{report.Id}";
        return Results.Created();
    }
}
