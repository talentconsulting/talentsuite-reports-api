using FluentAssertions;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;

namespace TalentConsulting.TalentSuite.Reports.UnitTests.Projects;

public class WhenValidatingGetProject
{
    [Fact]
    public void ThenShouldNotErrorWhenModelIsValidWhenGettingProjectById()
    {
        //Arrange
        var validator = new GetProjectByIdCommandValidator();
        var testModel = new GetProjectByIdCommand("a3226044-5c89-4257-8b07-f29745a22e2c");
            

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeFalse();
    }

    [Theory]
    [InlineData(default!)]
    [InlineData("")]
    public void ThenShouldErrorWhenModelIsNoptValidWhenGettingProjectById(string id)
    {
        //Arrange
        var validator = new GetProjectByIdCommandValidator();
        var testModel = new GetProjectByIdCommand(id);


        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeTrue();
    }
}
