using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateUser;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.UnitTests.Reports;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Users;

public class WhenUsingUserCommands : BaseCreateDbUnitTest
{
    [Fact]
    public async Task ThenCreateUser()
    {
        //Arrange
        var logger = new Mock<ILogger<CreateUserCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        UserDto testProject = GetTestUserDto();
        var command = new CreateUserCommand(testProject);
        var handler = new CreateUserCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

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
        var logger = new Logger<CreateUserCommandHandler>(new LoggerFactory());
        var handler = new CreateUserCommandHandler(GetApplicationDbContext(), _mapper, logger);
        var command = new CreateUserCommand(default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ThenUpdateUser()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbReport = new Report(_reportId, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<Risk>()
        {
            new Risk(_riskId, _reportId, "Risk Details 1", "Risk Mitigation 1", "Risk Status 1" )
        });
        var dbUser = new User(_userId, "First Name", "Last Name", "email@email.com", _usergroupId, new List<Report>() { dbReport });
            
        
        mockApplicationDbContext.Users.Add(dbUser);
        await mockApplicationDbContext.SaveChangesAsync();
        var testUser = GetTestUserDto();
        var logger = new Mock<ILogger<UpdateUserCommandHandler>>();

        var command = new UpdateUserCommand(_userId, testUser);
        var handler = new UpdateUserCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testUser.Id);
    }

    [Fact]
    public async Task ThenHandle_ThrowsException_WhenUserNotFound()
    {
        // Arrange
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbReport = new Report(_reportId, "Planned tasks 1", "Completed tasks 1", 1, DateTime.UtcNow, _projectId, _userId, new List<Risk>()
        {
            new Risk(_riskId, _reportId, "Risk Details 1", "Risk Mitigation 1", "Risk Status 1" )
        });
        var dbUser = new User(_userId, "First Name", "Last Name", "email@email.com", _usergroupId, new List<Report>() { dbReport });


        mockApplicationDbContext.Users.Add(dbUser);
        await mockApplicationDbContext.SaveChangesAsync();
        var logger = new Mock<ILogger<UpdateUserCommandHandler>>();
        var handler = new UpdateUserCommandHandler(mockApplicationDbContext, _mapper, logger.Object);
        var command = new UpdateUserCommand("someotherid", default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    public static UserDto GetTestUserDto()
    {
        return new UserDto(_userId, "First Name", "Last Name", "email@email.com", _usergroupId, new List<ReportDto>() { WhenUsingReportCommands.GetTestReportDto() });
    }

}
