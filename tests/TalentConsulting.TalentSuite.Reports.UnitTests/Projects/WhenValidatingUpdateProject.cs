using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Projects;

public class WhenValidatingUpdateProject
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("Id", new ProjectDto("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeFalse();
    }

    [Fact]
    public void ThenShouldErrorWhenHasNoId()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("", new ProjectDto("", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ProjectDto.Id").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoId()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("Id", new ProjectDto("", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ProjectDto.Id").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoName()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("Id", new ProjectDto("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", default!, "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ProjectDto.Name").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoContactNumber()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", new ProjectDto("a3226044-5c89-4257-8b07-f29745a22e2c", default!, "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ProjectDto.ContactNumber").Should().BeTrue();
    }
    [Fact]
    public void ThenShouldErrorWhenModelHasNoReferance()
    {
        //Arrange
        var validator = new UpdateProjectCommandValidator();
        var testModel = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", new ProjectDto("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", default!, new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ProjectDto.Reference").Should().BeTrue();
    }

    
}
