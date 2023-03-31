using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateClient;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateClient;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetClients;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Clients;

public class WhenUsingClientCommands : BaseCreateDbUnitTest
{
    [Fact]
    public async Task ThenCreateClient()
    {
        //Arrange
        var logger = new Mock<ILogger<CreateClientCommandHandler>>();
        var mockApplicationDbContext = GetApplicationDbContext();
        ClientDto testProject = GetTestClientDto();
        var command = new CreateClientCommand(testProject);
        var handler = new CreateClientCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

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
        var logger = new Logger<CreateClientCommandHandler>(new LoggerFactory());
        var handler = new CreateClientCommandHandler(GetApplicationDbContext(), _mapper, logger);
        var command = new CreateClientCommand(default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ThenUpdateClient()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbClient = new Client(_clientId, "Name", "Contact Name", "Contact Email", new List<ClientProject>() { new ClientProject(_clientProjectId, _clientId, _projectId) });
        mockApplicationDbContext.Clients.Add(dbClient);
        await mockApplicationDbContext.SaveChangesAsync();
        var testClient = GetTestClientDto();
        var logger = new Mock<ILogger<UpdateClientCommandHandler>>();

        var command = new UpdateClientCommand(_clientId, testClient);
        var handler = new UpdateClientCommandHandler(mockApplicationDbContext, _mapper, logger.Object);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(testClient.Id);
    }

    [Fact]
    public async Task ThenHandle_ThrowsException_WhenClientNotFound()
    {
        // Arrange
        var dbContext = GetApplicationDbContext();
        var logger = new Mock<ILogger<UpdateClientCommandHandler>>();
        var handler = new UpdateClientCommandHandler(dbContext, _mapper, logger.Object);
        var command = new UpdateClientCommand("someotherid", default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task ThenGetClient()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbClient = GetTestClient();
        mockApplicationDbContext.Clients.Add(dbClient);
        await mockApplicationDbContext.SaveChangesAsync();


        var command = new GetClientsCommand(1, 99);
        var handler = new GetClientsCommandHandler(mockApplicationDbContext);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items[0].Id.Should().Be(dbClient.Id);
        result.Items[0].Name.Should().Be(dbClient.Name);

    }

    [Fact]
    public async Task ThenGetClientWithNullRequest()
    {
        var mockApplicationDbContext = GetApplicationDbContext();
        var dbClient = GetTestClient();
        mockApplicationDbContext.Clients.Add(dbClient);
        await mockApplicationDbContext.SaveChangesAsync();
        var handler = new GetClientsCommandHandler(mockApplicationDbContext);

        //Act
        var result = await handler.Handle(new GetClientsCommand(1, 99), new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items[0].Id.Should().Be(dbClient.Id);
        result.Items[0].Name.Should().Be(dbClient.Name);
    }


    public static Client GetTestClient()
    {
        return new Client(_clientId, "Name", "Contact Name", "Contact Email", new List<ClientProject>() { new ClientProject(_clientProjectId, _clientId, _projectId) });
    }

    public static ClientDto GetTestClientDto()
    {
        return new ClientDto(_clientId, "Name", "Contact Name", "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) });
    }
}
