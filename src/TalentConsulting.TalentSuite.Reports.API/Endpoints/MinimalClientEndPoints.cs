using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateClient;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateClient;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetClients;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.API.Endpoints;

public class MinimalClientEndPoints
{
    public void RegisterClientEndPoints(WebApplication app)
    {
        app.MapPost("api/clients", [Authorize(Policy = "TalentConsultingUser")] async ([FromBody] ClientDto request, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                CreateClientCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Clients", "Create client") { Tags = new[] { "Clients" } });

        app.MapPut("api/clients/{id}", [Authorize(Policy = "TalentConsultingUser")] async (string id, [FromBody] ClientDto request, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalClientEndPoints> logger) =>
        {
            try
            {
                UpdateClientCommand command = new(id, request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred updating client (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Update Client", "Update Client By Id") { Tags = new[] { "Clients" } });

        app.MapGet("api/clients", [Authorize(Policy = "TalentConsultingUser")] async (int? pageNumber, int? pageSize, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetClientsCommand request = new(pageNumber, pageSize);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Clients", "Get Clients Paginated") { Tags = new[] { "Clients" } });
    }
}
