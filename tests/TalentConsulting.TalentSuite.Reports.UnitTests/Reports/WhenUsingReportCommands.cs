using Ardalis.GuardClauses;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateReport;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Reports;

public class WhenUsingReportCommands : BaseCreateDbUnitTest
{
    private IMapper _mapper { get; }

    const string _projectId = "a3226044-5c89-4257-8b07-f29745a22e2c";
    const string _reportId = "5698dbc0-a10c-43e5-9074-4ce6d6637778";
    const string _userId = "ce6edc11-3477-4b88-946d-598d5a7aa68a";
    const string _riskId = "41fef4ce-c85f-4273-8572-0222e471db63";

    public WhenUsingReportCommands()
    {
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(myProfile);
        });
        _mapper = new Mapper(configuration);
    }

    [Fact]
    public async Task ThenCreateReport()
    {
        //Arrange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(myProfile);
        });
        var mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateReportCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        ReportDto testProject = GetTestReportDto();
        var command = new CreateReportCommand(testProject);
        var handler = new CreateReportCommandHandler(mockApplicationDbContext, mapper, logger.Object);

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
        var context = GetApplicationDbContext();
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(myProfile);
        });
        var mapper = new Mapper(configuration);
        var logger = new Logger<CreateReportCommandHandler>(new LoggerFactory());
        var handler = new CreateReportCommandHandler(context, mapper, logger);
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
        var command = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", default!);

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
