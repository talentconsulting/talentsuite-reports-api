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

    const string _projectId = "a3226044-5c89-4257-8b07-f29745a22e2c";
    const string _reportId = "5698dbc0-a10c-43e5-9074-4ce6d6637778";
    const string _userId = "ce6edc11-3477-4b88-946d-598d5a7aa68a";
    const string _clientId = "1e68f5cd-2347-4b09-820e-3297605e3743";
    const string _riskId = "41fef4ce-c85f-4273-8572-0222e471db63";
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

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task ThenCreateProject(bool newProjectId)
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
        if (newProjectId)
        {
            var project = GetTestProject();
            mockApplicationDbContext.Projects.Add(project);
            mockApplicationDbContext.SaveChanges();
        }
        var testProject = GetTestProjectDto(newProjectId);
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
        var dbProject = GetTestProject(); 
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
        var dbProject = GetTestProject(); 
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
    public async Task ThenGetProject()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = GetTestProject();
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
    public async Task ThenGetProjectWithNullRequest()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = GetTestProject();
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

    [Fact]
    public async Task ThenGetProjectById()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbProject = GetTestProject();
        mockApplicationDbContext.Projects.Add(dbProject);
        await mockApplicationDbContext.SaveChangesAsync();


        var command = new GetProjectByIdCommand(dbProject.Id);
        var handler = new GetProjectByIdCommandHandler(mockApplicationDbContext);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("a3226044-5c89-4257-8b07-f29745a22e2c");
        result.Name.Should().Be(dbProject.Name);

    }

    [Fact]
    public async Task ThenGetProjectById_ThatDoesNotExist()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        
        var command = new GetProjectByIdCommand("8f145d0c-2b07-4beb-8a7f-d66055b88dc0");
        var handler = new GetProjectByIdCommandHandler(mockApplicationDbContext);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    public static Project GetTestProject()
    {
        return new Project(_projectId, "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>()
            {
                new ClientProject("d0ec8781-28ed-43dc-8840-e18ac1d255e8",_clientId,_projectId)
            },
            new List<Contact>()
            {
                new Contact("8585578d-8ac0-4613-96ff-89403c56a2c7", "firstname", "email@email.com", true, _projectId)
            },
            new List<Report>()
            {
                new Report(_reportId, "Planned tasks", "Completed tasks", 1, DateTime.UtcNow, _projectId, _userId,
                new List<Risk>()
                {
                    new Risk(_riskId, _reportId, "Risk Details", "Risk Mitigation", "Risk Status" )
                }
                )
            },
            new List<Sow>()
            {
                new Sow("946c4c15-913c-42e1-947d-b813b90f4d81", DateTime.UtcNow, new byte[] { 1 }, true, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), _projectId)
            });
    }

    public static ProjectDto GetTestProjectDto(bool changeProjectId = false)
    {
        string projectId = _projectId;
        if (changeProjectId)
        {
            projectId = Guid.NewGuid().ToString();
        }

        var risks = new List<RiskDto>()
        {
            new RiskDto(_riskId, _reportId, "Risk Details", "Risk Mitigation", "Risk Status" )
        };

        return new ProjectDto(projectId, "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProjectDto>()
            {
                new ClientProjectDto("d0ec8781-28ed-43dc-8840-e18ac1d255e8",_clientId,projectId)
            },
            new List<ContactDto>()
            {
                new ContactDto("8585578d-8ac0-4613-96ff-89403c56a2c7", "firstname", "email@email.com", true, projectId)
            },
            new List<ReportDto>()
            {
                new ReportDto("faf41f35-5da0-409d-968a-1e50e33345aa", DateTime.UtcNow.AddDays(-1), "Planned tasks", "Completed tasks", 1, DateTime.UtcNow, projectId, _userId,risks)
            },
            new List<SowDto>()
            {
                new SowDto("946c4c15-913c-42e1-947d-b813b90f4d81", DateTime.UtcNow, new byte[] { 1 }, true, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), projectId)
            });
    }
}
