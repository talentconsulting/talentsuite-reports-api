using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;
using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Users;

public class WhenValidatingCreateUser : BaseTestValidation
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(WhenUsingUserCommands.GetTestUserDto());

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
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(new UserDto(id, "First Name", "Last Name", "email@email.com", _usergroupId, new List<ReportDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "UserDto.Id").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoFirstname()
    {
        //Arrange
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(new UserDto(_userId, default!, "Last Name", "email@email.com", _usergroupId, new List<ReportDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "UserDto.Firstname").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoLastname()
    {
        //Arrange
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(new UserDto(_userId, "First Name", default!, "email@email.com", _usergroupId, new List<ReportDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "UserDto.Lastname").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoEmail()
    {
        //Arrange
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(new UserDto(_userId, "First Name", "Last Name", default!, _usergroupId, new List<ReportDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "UserDto.Email").Should().BeTrue();
    }

    [Fact]
    public void ThenShouldErrorWhenModelHasNoUserGroupId()
    {
        //Arrange
        var validator = new CreateUserCommandValidator();
        var testModel = new CreateUserCommand(new UserDto(_userId, "First Name", "Last Name", "email@email.com", default!, new List<ReportDto>()));

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any(x => x.PropertyName == "UserDto.UserGroupId").Should().BeTrue();
    }
}
