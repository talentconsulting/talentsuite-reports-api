using Microsoft.AspNetCore.Mvc;

namespace TalentConsulting.TalentSuite.ReportsApi.Endpoints;

internal sealed class GetInfoEndpoint : IApiEndpoint
{
    internal record InfoResponse(string Version);
    
    public static void Register(WebApplication app)
    {
        app.MapGet("/info", GetInfo)
            .WithTags("Service Status")
            .WithDescription("Returns basic service information")
        .WithOpenApi();
    }

    private static InfoResponse GetInfo([FromServices] IConfiguration configuration) => new (configuration["Version"] ?? "0.0.0");
}