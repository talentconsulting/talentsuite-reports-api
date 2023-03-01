﻿using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateReport;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Reports;

public class WhenUsingReportCommands : BaseCreateDbUnitTest
{

    [Fact]
    public async Task ThenCreateReport()
    {
        //Arrange
        var logger = new Mock<ILogger<CreateReportCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        ReportDto testProject = GetTestReportDto();
        var command = new CreateReportCommand(testProject);
        var handler = new CreateReportCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testProject.Id);
    }

    [Fact]
    public async Task ThenHandle_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Arrange
        var logger = new Logger<CreateReportCommandHandler>(new LoggerFactory());
        var handler = new CreateReportCommandHandler(GetApplicationDbContext(), _mapper, logger);
        var command = new CreateReportCommand(default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ThenUpdateReport()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbReport = new Report(_reportId, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<Risk>()
        {
            new Risk(_riskId, _reportId, "Risk Details 1", "Risk Mitigation 1", "Risk Status 1" )
        });
        mockApplicationDbContext.Reports.Add(dbReport);
        await mockApplicationDbContext.SaveChangesAsync();
        var testReport = GetTestReportDto();
        var logger = new Mock<ILogger<UpdateReportCommandHandler>>();

        var command = new UpdateReportCommand(_reportId, testReport);
        var handler = new UpdateReportCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testReport.Id);
    }

    [Fact]
    public async Task ThenHandle_ThrowsException_WhenReportNotFound()
    {
        // Arrange
        var dbContext = GetApplicationDbContext();
        var dbReport = new Report(_reportId, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<Risk>()
        {
            new Risk(_riskId, _reportId, "Risk Details 1", "Risk Mitigation 1", "Risk Status 1" )
        });
        dbContext.Reports.Add(dbReport);
        await dbContext.SaveChangesAsync();
        var logger = new Mock<ILogger<UpdateProjectCommandHandler>>();
        var handler = new UpdateProjectCommandHandler(dbContext, _mapper, logger.Object);
        var command = new UpdateProjectCommand("someotherid", default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    public static ReportDto GetTestReportDto()
    {
        var risks = new List<RiskDto>()
        {
            new RiskDto(_riskId, _reportId, "Risk Details", "Risk Mitigation", "Risk Status" )
        };

        return new ReportDto(_reportId, DateTime.UtcNow.AddDays(-1), "Planned tasks", "Completed tasks", 1, DateTime.UtcNow, _projectId, _userId, risks);
    }
}
