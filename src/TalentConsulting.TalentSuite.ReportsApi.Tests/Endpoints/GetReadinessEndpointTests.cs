namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints;

internal class GetReadinessEndpointTests : ServerFixtureBase
{
    [Test]
    public async Task Get_Should_Return_NoContent()
    {
        // act
        using var response = await Client.GetAsync("/readiness");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}