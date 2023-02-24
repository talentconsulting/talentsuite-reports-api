using Ardalis.Specification;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Helpers;

public static class EntityToDtoHelper
{
    public static ProjectDto ProjectDtoToProjectDto(Project entity)
    {
        return new ProjectDto(
             id: entity.Id,
          contactNumber: entity.ContactNumber,
        name: entity.Name,
          reference: entity.Reference,
          startDate: entity.StartDate,
          endDate: entity.EndDate,
          clientProjects: entity.ClientProjects.Select(clientProject => new ClientProjectDto(clientProject.Id, clientProject.ClientId, clientProject.ProjectId)).ToList(),
          contacts: entity.Contacts.Select(contact => new ContactDto(contact.Id, contact.Firstname, contact.Email, contact.ReceivesReport, contact.ProjectId)).ToList(),
          reports: entity.Reports.Select(report => new ReportDto(report.Id, (report.Created != null) ? report.Created.Value : DateTime.UtcNow, report.PlannedTasks, report.CompletedTasks, report.Weeknumber, report.SubmissionDate, report.ProjectId, report.UserId)).ToList(),
          sows: entity.Sows.Select(sow => new SowDto(sow.Id, (sow.Created != null) ? sow.Created.Value : DateTime.UtcNow, sow.File, sow.IsChangeRequest, sow.SowStartDate, sow.SowEndDate, sow.ProjectId)).ToList()
          );
    }
}
