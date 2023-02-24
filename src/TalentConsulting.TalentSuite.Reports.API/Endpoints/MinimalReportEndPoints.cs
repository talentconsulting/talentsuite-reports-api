using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.API.Endpoints;

public class MinimalReportEndPoints
{
    public void RegisterReportEndPoints(WebApplication app)
    {
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
