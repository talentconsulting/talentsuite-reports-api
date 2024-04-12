using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints.Reports;

[TestFixture]
public class DeleteReportEndpointTests : ServerFixtureBase
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
    public async Task Delete_Returns_NotFound()
    {
        // act
        using var response = await Client.DeleteAsync($"/reports/{Guid.NewGuid()}");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Delete_Removes_Report_From_DataStore()
    {
        // arrange
        var report = TestData.Client1.Reports.First();
        Report? target = new ();

        // act
        using var deleteResponse = await Client.DeleteAsync($"/reports/{report.Id}");
        await Server.QueryDbAsync(async ctx => target = await ctx.Reports.FindAsync(report.Id));

        // assert
        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        target.ShouldBeNull();
    }
}