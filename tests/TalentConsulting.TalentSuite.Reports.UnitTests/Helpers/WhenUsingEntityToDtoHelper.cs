using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Helpers;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Helpers;

public class WhenUsingEntityToDtoHelper
{
    [Fact]
    public void ProjectDtoToProjectDtoReutrnsFulProjectDtoObject()
    { 

        DateTime dtStartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dtEndDate = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Utc);
        const string projectId = "84ffdcb4-c715-47f7-a9e0-3ca1d8648c80";
        const string userId = "075d11b3-00a5-4f9d-ae8e-b83f7169eb7c";
        const string reportId = "f5b83bda-79b0-49c2-95d2-ef9880ecbec1";
        const string clientProjectId = "56eb38b6-b271-4586-a156-8367c58e0cbd";
        const string contactId = "61e0661d-6729-450c-bbe7-f56c7e3fe01b";
        const string riskId = "5c687803-36b1-4342-bbeb-15eca2fef1ce";
        const string sowId = "24bce4f6-83f1-4b75-ab8c-6176d43c259d";
        var clientProjects = new List<ClientProject>
        {
            new ClientProject(new Guid(clientProjectId), new Guid(userId), new Guid(projectId))
        };
        var contacts = new List<Contact>
        {
            new Contact(new Guid(contactId), "Firstname", "Email", true, new Guid(projectId))
        };
        var risks = new List<Risk>
        {
            new Risk(new Guid(riskId),new Guid(reportId), "Risk Details", "Risk Mitigation", "Rag Status")
        };
        var reports = new List<Report>
        {
            new Report(new Guid(reportId), "Planned Tasks", "Completed Tasks", 1, dtStartDate, new Guid(projectId), new Guid(userId), risks)
        };
        SowFile file = new SowFile { Id = new Guid("c5fe11c4-c345-4ffd-99b3-1b3b2a65403e"), Mimetype = "Mimetype", Filename = "Filename", File = new byte[0], Size = 0, SowId = new Guid(sowId) };
        var sows = new List<Sow>
        {
            new Sow(new Guid(sowId), dtStartDate, new List<SowFile>{ file }, true, dtStartDate, dtEndDate, new Guid(projectId))
        };
        var project = new Project(new Guid(projectId), "Contact Number", "Name", "Reference", dtStartDate, dtEndDate, clientProjects, contacts, reports, sows);

        var sowFileDto = new SowFileDto { Id = "c5fe11c4-c345-4ffd-99b3-1b3b2a65403e", Mimetype = "Mimetype", Filename = "Filename", File = new byte[0], Size = 0, SowId = sowId };

        var expectedProjectDto = new ProjectDto(projectId, "Contact Number", "Name", "Reference", dtStartDate, dtEndDate, EntityToDtoHelper.GetClientProjects(clientProjects),
            new List<ContactDto> { new ContactDto(contactId, "Firstname", "Email", true, projectId) },
            EntityToDtoHelper.GetReports(reports),
            new List<SowDto> { new SowDto(sowId, dtStartDate, new List<SowFileDto> { sowFileDto }, true, dtStartDate, dtEndDate, projectId) });

        var projectDto = EntityToDtoHelper.ProjectToProjectDto(project);

        projectDto.Should().BeEquivalentTo(expectedProjectDto, options => options.Excluding(x => x.Reports));
        projectDto.Reports.Should().BeEquivalentTo(expectedProjectDto.Reports, options => options.Excluding(x => x.Created));

    }
}
