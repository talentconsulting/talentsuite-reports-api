﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

internal sealed class DeleteReportEndpoint : IApiEndpoint
{
    [ExcludeFromCodeCoverage]
    public static void Register(WebApplication app)
    {
        app.MapDelete("/reports/{id:guid}", DeleteReport)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Reporting")
            .WithDescription("The report to delete")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> DeleteReport(
        [FromServices] IReportsProvider reportsProvider,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        return await reportsProvider.Delete(id, cancellationToken)
            ? Results.NoContent()
            : Results.NotFound();
    }
}
