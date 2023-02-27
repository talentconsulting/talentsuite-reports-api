using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateReport;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.API.Endpoints;

public class MinimalReportEndPoints
{
    public void RegisterReportEndPoints(WebApplication app)
    {
        app.MapPost("api/reports", [Authorize(Policy = "TalentConsultingUser")] async ([FromBody] ReportDto request, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                CreateReportCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Reports", "Create report") { Tags = new[] { "Reports" } });

        app.MapPut("api/reports/{id}", [Authorize(Policy = "TalentConsultingUser")] async (string id, [FromBody] ReportDto request, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalProjectEndPoints> logger) =>
        {
            try
            {
                UpdateReportCommand command = new(id, request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred updating report (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Update Report", "Update Report By Id") { Tags = new[] { "Reports" } });

        app.MapGet("api/reports", [Authorize(Policy = "TalentConsultingUser")] async (int? pageNumber, int? pageSize, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetReportsCommand request = new(pageNumber, pageSize);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Reports", "Get Reports Paginated") { Tags = new[] { "Reports" } });
    }
}
