using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Projects;

public class WhenValidatingCreateProject : BaseTestValidation
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new CreateProjectCommandValidator();
        var testModel = new CreateProjectCommand(new ProjectDto(_projectId, "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeFalse();
    }

    [Theory]
    [InlineData(default!)]
    [InlineData("")]
    public void ThenShouldErrorWhenModelHasNoId(string id)
    {
        //Arrange
        var validator = new CreateProjectCommandValidator();
        var testModel = new CreateProjectCommand(new ProjectDto(id, "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
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
        var validator = new CreateProjectCommandValidator();
        var testModel = new CreateProjectCommand(new ProjectDto(_projectId, "0121 111 2222", default!, "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
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
        var validator = new CreateProjectCommandValidator();
        var testModel = new CreateProjectCommand(new ProjectDto(_projectId, default!, "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
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
        var validator = new CreateProjectCommandValidator();
        var testModel = new CreateProjectCommand(new ProjectDto(_projectId, "0121 111 2222", "Social work CPD", default!, new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
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
