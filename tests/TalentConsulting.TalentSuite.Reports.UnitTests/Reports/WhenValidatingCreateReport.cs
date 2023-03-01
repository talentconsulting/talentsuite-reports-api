using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Reports;

public class WhenValidatingCreateReport : BaseTestValidation
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(WhenUsingReportCommands.GetTestReportDto());

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
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(new ReportDto(id, DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<RiskDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ReportDto.Id").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoPlannedTasks()
    {
        //Arrange
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(new ReportDto(_reportId, DateTime.UtcNow, default!, "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<RiskDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ReportDto.PlannedTasks").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoProjectId()
    {
        //Arrange
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(new ReportDto(_reportId, DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, default!, _userId, new List<RiskDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ReportDto.ProjectId").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoUserId()
    {
        //Arrange
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(new ReportDto(_reportId, DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, default!, new List<RiskDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ReportDto.UserId").Should().BeTrue();
    }
}
