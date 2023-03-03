using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.UpdateClient;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Clients;

public class WhenValidatingUpdateClient : BaseTestValidation
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(_reportId, WhenUsingClientCommands.GetTestClientDto());

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeFalse();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoClientDtoId()
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(_reportId, new ClientDto("", "Name", "Contact Name", "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) }));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ClientDto.Id").Should().BeTrue();
    }

    [Theory]
    [InlineData(default!)]
    [InlineData("")]
    public void ThenShouldErrorWhenModelHasNoId(string id)
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(id, new ClientDto(_reportId, "Name", "Contact Name", "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) }));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "Id").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoName()
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(_reportId, new ClientDto(_reportId, default!, "Contact Name", "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) }));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ClientDto.Name").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoContactName()
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(_reportId, new ClientDto(_reportId, "Name", default!, "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) }));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ClientDto.ContactName").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasContactEmail()
    {
        //Arrange
        var validator = new UpdateClientCommandValidator();
        var testModel = new UpdateClientCommand(_reportId, new ClientDto(_reportId, "Name", "Contact Name", default!, new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) }));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "ClientDto.ContactEmail").Should().BeTrue();
    }
}