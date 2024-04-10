using Microsoft.AspNetCore.Mvc;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints;

public sealed class GetReadinessEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);

    public static void Register(WebApplication app)
    {
        app.MapGet("/readiness", GetReadiness)
            .WithTags("Service Status")
            .WithDescription("Returns whether the service is ready")
            .WithOpenApi();
    }

    // TODO: implement whatever constitutes a readiness check
    private static IResult GetReadiness([FromServices] IConfiguration configuration) => Results.Ok();
}
