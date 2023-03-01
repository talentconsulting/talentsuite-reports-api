using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateUser;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetUsers;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.API.Endpoints;

public class MinimalUserEndPoints
{
    public void RegisterUserEndPoints(WebApplication app)
    {
        app.MapPost("api/users", [Authorize(Policy = "TalentConsultingUser")] async ([FromBody] UserDto request, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                CreateUserCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Users", "Create user") { Tags = new[] { "Users" } });

        app.MapPut("api/users/{id}", [Authorize(Policy = "TalentConsultingUser")] async (string id, [FromBody] UserDto request, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalProjectEndPoints> logger) =>
        {
            try
            {
                UpdateUserCommand command = new(id, request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred updating user (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Update User", "Update User By Id") { Tags = new[] { "Users" } });

        app.MapGet("api/users", [Authorize(Policy = "TalentConsultingUser")] async (int? pageNumber, int? pageSize, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetUsersCommand request = new(pageNumber, pageSize);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Users", "Get Users Paginated") { Tags = new[] { "Users" } });
    }
}
