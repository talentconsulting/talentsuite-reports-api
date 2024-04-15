using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints.Reports;

[TestFixture]
public class GetReportEndpointTests : ServerFixtureBase
{
    [SetUp]
    public async Task Setup()
    {
        await Server.ResetDbAsync(async ctx => {
            ctx.Reports.AddRange(TestData.Client1.Reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });
    }

    [Test]
    public async Task Get_Returns_NotFound()
    {
        // act
        using var response = await Client.GetAsync($"/reports/{Guid.NewGuid()}");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Get_Returns_Report()
    {
        // arrange
        var expected = TestData.Client1.Reports.First();

        // act
        using var response = await Client.GetAsync($"/reports/{expected.Id}");
        var actual = await response.Content.ReadFromJsonAsync<ReportDto>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        actual.ShouldNotBeNull();
        actual.ShouldBeEquivalentTo(expected.ToReportDto());
    }
}