using Ardalis.GuardClauses;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateProject;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Projects;

public class WhenUsingProjectCommands : BaseCreateDbUnitTest
{
    private IMapper _mapper { get; }
    public WhenUsingProjectCommands()
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
    public async Task ThenCreateProject()
    {
        //Arrange
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(myProfile);
        });
        var mapper = new Mapper(configuration);
        var logger = new Mock<ILogger<CreateProjectCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        var testProject = GetTestProjectDto();
        var command = new CreateProjectCommand(testProject);
        var handler = new CreateProjectCommandHandler(mockApplicationDbContext, mapper, logger.Object);

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
        var logger = new Logger<CreateProjectCommandHandler>(new LoggerFactory());
        var handler = new CreateProjectCommandHandler(context, mapper, logger);
        var command = new CreateProjectCommand(default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ThenUpdateProject()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = new Project("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>(),
            new List<Contact>(),
            new List<Report>(),
            new List<Sow>());
        mockApplicationDbContext.Projects.Add(dbProject);
        await mockApplicationDbContext.SaveChangesAsync();
        var testProject = GetTestProjectDto();
        var logger = new Mock<ILogger<UpdateProjectCommandHandler>>();

        var command = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", testProject);
        var handler = new UpdateProjectCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testProject.Id);
    }

    [Fact]
    public async Task ThenHandle_ThrowsException_WhenProjectNotFound()
    {
        // Arrange
        var dbContext = GetApplicationDbContext();
        var dbProject = new Project("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>(),
            new List<Contact>(),
            new List<Report>(),
            new List<Sow>());
        dbContext.Projects.Add(dbProject);
        await dbContext.SaveChangesAsync();
        var logger = new Mock<ILogger<UpdateProjectCommandHandler>>();
        var handler = new UpdateProjectCommandHandler(dbContext, _mapper, logger.Object);
        var command = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task ThenHandle_ThrowsNotFoundException_WhenProjectIdNotFound()
    {
        // Arrange
        var dbContext = GetApplicationDbContext();
        var logger = new Mock<ILogger<UpdateProjectCommandHandler>>();
        var handler = new UpdateProjectCommandHandler(dbContext, _mapper, logger.Object);
        var command = new UpdateProjectCommand("a3226044-5c89-4257-8b07-f29745a22e2c", default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task ThenGetTaxonomies()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = new Project("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>(),
            new List<Contact>(),
            new List<Report>(),
            new List<Sow>());
        mockApplicationDbContext.Projects.Add(dbProject);
        await mockApplicationDbContext.SaveChangesAsync();


        var command = new GetProjectsCommand(1,99);
        var handler = new GetProjectsCommandHandler(mockApplicationDbContext);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items[0].Id.Should().Be("a3226044-5c89-4257-8b07-f29745a22e2c");
        result.Items[0].Name.Should().Be(dbProject.Name);
        
    }

    [Fact]
    public async Task ThenGetTaxonomiesWithNullRequest()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = new Project("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>(),
            new List<Contact>(),
            new List<Report>(),
            new List<Sow>());
        mockApplicationDbContext.Projects.Add(dbProject);
        await mockApplicationDbContext.SaveChangesAsync();
        var handler = new GetProjectsCommandHandler(mockApplicationDbContext);

        //Act
        var result = await handler.Handle(new GetProjectsCommand(1,99), new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items[0].Id.Should().Be("a3226044-5c89-4257-8b07-f29745a22e2c");
        result.Items[0].Name.Should().Be(dbProject.Name);
    }

    public static ProjectDto GetTestProjectDto()
    {
        return new ProjectDto("a3226044-5c89-4257-8b07-f29745a22e2c", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>(),
            new List<ContactDto>(),
            new List<ReportDto>(),
            new List<SowDto>());
    }
}
