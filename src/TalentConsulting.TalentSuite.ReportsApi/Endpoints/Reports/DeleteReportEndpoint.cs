using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentConsulting.TalentSuite.ReportsApi.Db;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints.Reports;

public sealed class DeleteReportEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapDelete("/reports/{id:guid}", DeleteReport)
            .Produces(200)
            .Produces(404)
            .Produces(500)
            .WithTags("Reporting")
            .WithDescription("The report to delete")
            .WithOpenApi();
    }

    [Authorize(Policy = "TalentConsultingUser")]
    private static async Task<IResult> DeleteReport(
        [FromServices] IReportsProvider reportsProvider,
        Guid id,
        CancellationToken cancellationToken)
    {
        return await reportsProvider.Delete(id, cancellationToken)
            ? Results.Ok()
            : Results.NotFound();
    }
}
