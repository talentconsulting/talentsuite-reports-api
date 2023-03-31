﻿using Ardalis.Specification;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Helpers;

public static class EntityToDtoHelper
{
    public static ProjectDto ProjectDtoToProjectDto(Project entity)
    {
        return new ProjectDto(
             id: entity.id,
          contactNumber: entity.ContactNumber,
          name: entity.Name,
          reference: entity.Reference,
          startDate: entity.StartDate,
          endDate: entity.EndDate,
          clientProjects: entity.ClientProjects.Select(clientProject => new ClientProjectDto(clientProject.id, clientProject.ClientId, clientProject.ProjectId)).ToList(),
          contacts: entity.Contacts.Select(contact => new ContactDto(contact.id, contact.Firstname, contact.Email, contact.ReceivesReport, contact.ProjectId)).ToList(),
          reports: entity.Reports.Select(report => new ReportDto(report.id, (report.created != null) ? report.created.Value : DateTime.UtcNow, report.PlannedTasks, report.CompletedTasks, report.Weeknumber, report.SubmissionDate, report.ProjectId, report.UserId, GetRisks(report.Risks))).ToList(),
          sows: entity.Sows.Select(sow => new SowDto(sow.id, (sow.created != null) ? sow.created.Value : DateTime.UtcNow, sow.File, sow.IsChangeRequest, sow.SowStartDate, sow.SowEndDate, sow.ProjectId)).ToList()
          );
    }

    public static List<ReportDto> GetReports(ICollection<Report> reports)
    {
        return reports.Select(x => new ReportDto(x.id, (x.created != null) ? x.created.Value : DateTime.UtcNow, x.PlannedTasks, x.CompletedTasks, x.Weeknumber, x.SubmissionDate, x.ProjectId, x.UserId, GetRisks(x.Risks))).ToList();
    }

    public static List<RiskDto> GetRisks(ICollection<Risk> risks)
    {
        return risks.Select(x => new RiskDto(x.id, x.ReportId, x.RiskDetails, x.RiskMitigation, x.RagStatus)).ToList();
    }

    public static List<ClientProjectDto> GetClientProjects(ICollection<ClientProject> clientProjects)
    {
        return clientProjects.Select(x => new ClientProjectDto(x.id, x.ClientId, x.ProjectId)).ToList();
    }
}
