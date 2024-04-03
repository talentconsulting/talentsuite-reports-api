﻿using Ardalis.Specification;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Helpers;

public static class EntityToDtoHelper
{
    public static ProjectDto ProjectToProjectDto(Project entity)
    {
        return new ProjectDto(
             id: entity.Id.ToString(),
          contractNumber: entity.ContractNumber,
          name: entity.Name,
          reference: entity.Reference,
          startDate: entity.StartDate,
          endDate: entity.EndDate,
          clientProjects: entity.ClientProjects.Select(clientProject => new ClientProjectDto(clientProject.Id.ToString(), clientProject.ClientId.ToString(), clientProject.ProjectId.ToString())).ToList(),
          contacts: entity.Contacts.Select(contact => new ContactDto(contact.Id.ToString(), contact.Firstname, contact.Email, contact.ReceivesReport, contact.ProjectId.ToString())).ToList(),
          reports: entity.Reports.Select(report => new ReportDto(report.Id.ToString(), (report.Created != null) ? report.Created.Value : DateTime.UtcNow, report.PlannedTasks, report.CompletedTasks, report.Weeknumber, report.SubmissionDate, report.ProjectId.ToString(), report.UserId.ToString(), GetRisks(report.Risks))).ToList(),
          sows: entity.Sows.Select(sow => new SowDto(sow.Id.ToString(), (sow.Created != null) ? sow.Created.Value : DateTime.UtcNow, GetSowFiles(sow.Files), sow.IsChangeRequest, sow.SowStartDate, sow.SowEndDate, sow.ProjectId.ToString())).ToList()
          );
    }

    public static List<ReportDto> GetReports(ICollection<Report> reports)
    {
        return reports.Select(x => new ReportDto(x.Id.ToString(), (x.Created != null) ? x.Created.Value : DateTime.UtcNow, x.PlannedTasks, x.CompletedTasks, x.Weeknumber, x.SubmissionDate, x.ProjectId.ToString(), x.UserId.ToString(), GetRisks(x.Risks))).ToList();
    }

    public static List<RiskDto> GetRisks(ICollection<Risk> risks)
    {
        return risks.Select(x => new RiskDto(x.Id.ToString(), x.ReportId.ToString(), x.RiskDetails, x.RiskMitigation, x.RagStatus)).ToList();
    }

    public static List<ClientProjectDto> GetClientProjects(ICollection<ClientProject> clientProjects)
    {
        return clientProjects.Select(x => new ClientProjectDto(x.Id.ToString(), x.ClientId.ToString(), x.ProjectId.ToString())).ToList();
    }

    public static List<SowFileDto> GetSowFiles(ICollection<SowFile> files)
    {
        return files.Select(x => new SowFileDto { Id = x.Id.ToString(), Mimetype = x.Mimetype, Filename = x.Filename, Size = x.Size, SowId = x.SowId.ToString(), File = x.File }).ToList();
    }
}
