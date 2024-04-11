using TalentConsulting.TalentSuite.ReportsApi.Endpoints;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints;

internal class GetInfoEndpointTests : ServerFixtureBase
{
    [Test]
    public async Task Get_Should_Return_Ok()
    {
        // act
        using var response = await Client.GetAsync("/info");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Get_Should_Return_Information()
    {
        // act
        using var response = await Client.GetAsync("/info");
        var info = await response.Content.ReadFromJsonAsync<GetInfoEndpoint.InfoResponse>();

        // assert
        info.ShouldNotBeNull();
        info.Version.ShouldBe("0.0.0");
    }
}