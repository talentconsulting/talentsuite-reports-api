using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Reports;

public class WhenValidatingCreateReport
{
    const string _projectId = "a3226044-5c89-4257-8b07-f29745a22e2c";
    const string _reportId = "5698dbc0-a10c-43e5-9074-4ce6d6637778";
    const string _userId = "ce6edc11-3477-4b88-946d-598d5a7aa68a";

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

    [Fact]
    public void ThenShouldErrorWhenModelHasNoId()
    {
        //Arrange
        var validator = new CreateReportCommandValidator();
        var testModel = new CreateReportCommand(new ReportDto("", DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<RiskDto>()));

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
        var testModel = new CreateReportCommand(new ReportDto("", DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, default!, _userId, new List<RiskDto>()));

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
        var testModel = new CreateReportCommand(new ReportDto("", DateTime.UtcNow, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, default!, new List<RiskDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ReportDto.UserId").Should().BeTrue();
    }
}
