using static TalentConsulting.TalentSuite.ReportsApi.Endpoints.Requests.GetReportsEndpoint;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints.Reports;

[TestFixture]
public class GetReportsEndpointTests : ServerFixtureBase
{
    [TestCase("1", "1", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("-1", "-1", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("999999999", "999999999", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    public async Task Get_Returns_OK(string page, string pageSize, string projectId)
    {
        // arrange
        await Server.ResetDbAsync(async ctx => {
            var reports = TestData.Client1.GenerateNewReports(1000);
            await ctx.Reports.AddRangeAsync(reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });

        // act
        using var response = await Client.GetAsync($"/reports?pageSize={pageSize}&page={page}&projectId={projectId}");
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [TestCase("9999999999", "9999999999", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("foo", "1", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("", "1", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("1", "", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("1", "foo", "C00211ED-256F-4067-99E0-E1C4316A28D7")]
    [TestCase("1", "1", "")]
    public async Task Get_Returns_BadRequest(string page, string pageSize, string projectId)
    {
        // arrange
        await Server.ResetDbAsync(async ctx => {
            var reports = TestData.Client1.GenerateNewReports(1000);
            await ctx.Reports.AddRangeAsync(reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });

        // act
        using var response = await Client.GetAsync($"/reports?pageSize={pageSize}&page={page}&projectId={projectId}");
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [TestCase("-1", "-1", 1, 1)]
    [TestCase("0", "0", 1, 1)]
    [TestCase("1", "9999999", 1, 100)]
    [TestCase("9999999", "1", 999, 1)]
    public async Task Get_Handles_Out_Of_Range_Paging(string page, string pageSize, int expectedPage, int expectedPageSize)
    {
        // arrange
        await Server.ResetDbAsync(async ctx => {
            var reports = TestData.Client1.GenerateNewReports(1000);
            await ctx.Reports.AddRangeAsync(reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });

        // act
        using var response = await Client.GetAsync($"/reports?pageSize={pageSize}&page={page}&projectId={TestData.Client1.ProjectId}");
        var reportsResponse = await response.Content.ReadFromJsonAsync<ReportsResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        reportsResponse.ShouldNotBeNull();
        reportsResponse.PageInfo.Page.ShouldBe(expectedPage);
        reportsResponse.PageInfo.PageSize.ShouldBe(expectedPageSize);
    }

    [TestCase("2", "100", 1, 100, 50, 50)] // edge case
    [TestCase("5", "5", 5, 5, 5, 50)]
    [TestCase("7", "3", 7, 3, 3, 50)]
    [TestCase("21", "4", 13, 4, 2, 50)]
    [TestCase("51", "1", 50, 1, 1, 50)] // edge case
    public async Task Get_Handles_Paging(string page, string pageSize, int expectedPage, int expectedPageSize, int expectedCount, int expectedTotal)
    {
        // arrange
        await Server.ResetDbAsync(async ctx => {
            var reports = TestData.Client1.GenerateNewReports(50);
            await ctx.Reports.AddRangeAsync(reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });

        // act
        using var response = await Client.GetAsync($"/reports?pageSize={pageSize}&page={page}&projectId={TestData.Client1.ProjectId}");
        var reportsResponse = await response.Content.ReadFromJsonAsync<ReportsResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        reportsResponse.ShouldNotBeNull();
        reportsResponse.PageInfo.Page.ShouldBe(expectedPage);
        reportsResponse.PageInfo.PageSize.ShouldBe(expectedPageSize);
        reportsResponse.Reports.Count().ShouldBe(expectedCount);
        reportsResponse.PageInfo.TotalCount.ShouldBe(expectedTotal);
    }

    [Test]
    public async Task Get_Filters_By_ProjectId()
    {
        // arrange
        await Server.ResetDbAsync(async ctx => {
            var reports = TestData.Client1.GenerateNewReports(50);
            await ctx.Reports.AddRangeAsync(reports);

            reports = TestData.Client2.GenerateNewReports(50);
            await ctx.Reports.AddRangeAsync(reports);

            await ctx.SaveChangesAsync(CancellationToken.None);
        });

        // act
        using var response = await Client.GetAsync($"/reports?pageSize=10&page=1&projectId={TestData.Client2.ProjectId}");
        var reportsResponse = await response.Content.ReadFromJsonAsync<ReportsResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        reportsResponse.ShouldNotBeNull();
        reportsResponse.Reports.All(x => x.ProjectId == TestData.Client2.ProjectId).ShouldBeTrue();
        reportsResponse.PageInfo.Page.ShouldBe(1);
        reportsResponse.PageInfo.PageSize.ShouldBe(10);
        reportsResponse.PageInfo.TotalCount.ShouldBe(50);
    }

    [Test]
    public async Task Get_When_No_Data_Returns_Correct_Paging_Info()
    {
        // act
        using var response = await Client.GetAsync($"/reports?pageSize=10&page=1&projectId={TestData.Client2.ProjectId}");
        var reportsResponse = await response.Content.ReadFromJsonAsync<ReportsResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        reportsResponse.ShouldNotBeNull();
        reportsResponse.PageInfo.Page.ShouldBe(1);
        reportsResponse.PageInfo.PageSize.ShouldBe(10);
        reportsResponse.PageInfo.TotalCount.ShouldBe(0);
        reportsResponse.PageInfo.First.ShouldBe(0);
        reportsResponse.PageInfo.Last.ShouldBe(0);
    }
}