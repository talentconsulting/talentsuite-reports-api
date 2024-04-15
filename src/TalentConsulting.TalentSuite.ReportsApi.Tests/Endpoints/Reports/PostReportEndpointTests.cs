using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints.Reports;

[TestFixture]
public class PostReportEndpointTests : ServerFixtureBase
{
    [SetUp]
    public async Task Setup()
    {
        await Server.ResetDbAsync(async ctx => {
            ctx.Reports.AddRange(TestData.Client1.Reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });
    }

    public sealed record TestCreateReportDto(
        string? ClientId,
        string? ProjectId,
        string? SowId,
        string? Completed,
        string? Planned,
        IEnumerable<TestCreateRiskDto?>? Risks,
        string? Status
    );

    public sealed record TestCreateRiskDto(string? Description, string? Mitigation, string? Status);

    public static IEnumerable<object[]> BadRequestTestCases
    {
        get
        {
            var empty = Guid.Empty.ToString();
            var projectId = TestContext.CurrentContext.Random.NextGuid().ToString();
            var clientId = TestContext.CurrentContext.Random.NextGuid().ToString();
            var sowId = TestContext.CurrentContext.Random.NextGuid().ToString();
            var report = new TestCreateReportDto(clientId, projectId, sowId, string.Empty, string.Empty, [], ReportStatus.Submitted.ToString());
            var risk = new TestCreateRiskDto("A Description", "A Mitigation", "Green");

            yield return new object[] { report with { ClientId = empty }, "'Client Id' must not be empty" };
            yield return new object[] { report with { ClientId = null }, "BadHttpRequestException" };
            yield return new object[] { report with { ProjectId = empty }, "'Project Id' must not be empty" };
            yield return new object[] { report with { ProjectId = null }, "BadHttpRequestException" };
            yield return new object[] { report with { SowId = empty }, "'Sow Id' must not be empty" };
            yield return new object[] { report with { SowId = null }, "BadHttpRequestException" };
            yield return new object[] { report with { Status = string.Empty }, "BadHttpRequestException" };
            yield return new object[] { report with { Status = "Invalid" }, "BadHttpRequestException" };
            yield return new object[] { report with { Status = null }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = null }, "'Risks' must not be empty" };
            yield return new object[] { report with { Risks = [null] }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = [risk with { Description = null }] }, "'Description' must not be empty" };
            yield return new object[] { report with { Risks = [risk with { Description = string.Empty }] }, "'Description' must not be empty" };
            yield return new object[] { report with { Risks = [risk with { Status = null }] }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = [risk with { Status = string.Empty }] }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = [risk with { Status = "Invalid" }] }, "BadHttpRequestException" };
        }
    }

    [Test]
    [TestCaseSource(nameof(BadRequestTestCases))]
    public async Task Post_Returns_BadRequest(TestCreateReportDto report, string expectedError)
    {
        // arrange
        var content = JsonContent.Create(report);

        // act
        using var response = await Client.PostAsync($"/reports", content);
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        body.ShouldContain(expectedError);
    }

    [Test]
    public async Task Post_Returns_Created()
    {
        // arrange
        var risk = new CreateRiskDto("A description", null, RiskStatus.Green);
        var report = new CreateReportDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, [risk], ReportStatus.Submitted);
        var expected = report.ToEntity();
        Report? actual = null;

        // act
        using var response = await Client.PostAsync($"/reports", JsonContent.Create(report));
        var id = response.Headers.Location?.OriginalString.Split('/')[^1];
        if (Guid.TryParse(id, out Guid reportId))
        {
            // Copy the ids across as they're the only item our source doesn't have
            expected.Id = reportId;
            await Server.QueryDbAsync(async ctx => actual = await ctx.Reports.FindAsync(reportId));
            expected.Risks.First().Id = actual?.Risks.First().Id ?? Guid.Empty;
        }

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        actual.ShouldNotBeNull().ShouldBeEquivalentTo(expected);
    }
}