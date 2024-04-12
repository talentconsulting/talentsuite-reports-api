using TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests.Endpoints.Reports;

[TestFixture]
public class PutReportEndpointTests : ServerFixtureBase
{
    [SetUp]
    public async Task Setup()
    {
        await Server.ResetDbAsync(async ctx => {
            ctx.Reports.AddRange(TestData.Client1.Reports);
            await ctx.SaveChangesAsync(CancellationToken.None);
        });
    }

    public sealed record TestReportDto(
        string? Id,
        string? ClientId,
        string? ProjectId,
        string? SowId,
        string? Completed,
        string? Planned,
        IEnumerable<TestRiskDto?>? Risks,
        string? Status
    )
    {
        public static TestReportDto From(Report report)
        {
            return new TestReportDto(
                report.Id.ToString(),
                report.ClientId.ToString(),
                report.ProjectId.ToString(),
                report.SowId.ToString(),
                report.Completed,
                report.Planned,
                report.Risks.Select(TestRiskDto.From),
                report.Status.ToString());
        }
    }

    public sealed record TestRiskDto(string? Id, string? Description, string? Mitigation, string? Status)
    {
        public static TestRiskDto From(Risk risk)
        {
            return new TestRiskDto(
                risk.Id.ToString(),
                risk.Description,
                risk.Mitigation,
                risk.Status.ToString());
        }
    }

    public static IEnumerable<object[]> BadRequestTestCases
    {
        get
        {
            var empty = Guid.Empty.ToString();
            var report = TestReportDto.From(TestData.Client1.Reports.First());
            var risk = report.Risks!.First()!;

            yield return new object[] { report with { Id = empty }, "'Id' must not be empty" };
            yield return new object[] { report with { Id = null }, "BadHttpRequestException" };
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
            yield return new object[] { report with { Risks = [null] }, "'Risks' must not be empty" };
            yield return new object[] { report with { Risks = [risk with { Description = null }] }, "'Description' must not be empty" };
            yield return new object[] { report with { Risks = [risk with { Description = string.Empty }] }, "'Description' must not be empty" };
            yield return new object[] { report with { Risks = [risk with { Status = null }] }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = [risk with { Status = string.Empty }] }, "BadHttpRequestException" };
            yield return new object[] { report with { Risks = [risk with { Status = "Invalid" }] }, "BadHttpRequestException" };
        }
    }

    [Test]
    [TestCaseSource(nameof(BadRequestTestCases))]
    public async Task PostReport_Returns_BadRequest(TestReportDto report, string expectedError)
    {
        // arrange
        var content = JsonContent.Create(report);

        // act
        using var response = await Client.PutAsync($"/reports", content);
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        body.ShouldContain(expectedError);
    }

    [Test]
    public async Task PostReport_Returns_NotFound()
    {
        // arrange
        var report = new ReportDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, [], ReportStatus.Submitted);

        // act
        using var response = await Client.PutAsync($"/reports", JsonContent.Create(report));

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PostReport_Returns_NoContent()
    {
        // arrange
        var report = TestData.Client1.Reports.First().ToReportDto() with
        {
            ClientId = TestContext.CurrentContext.Random.NextGuid(),
            ProjectId = TestContext.CurrentContext.Random.NextGuid(),
            SowId = TestContext.CurrentContext.Random.NextGuid(),
            Completed = "Foo",
            Planned = "Foo",
            Risks = [],
            Status = ReportStatus.Retracted
        };
        var expected = report.ToEntity();
        Report? actual = new Report();

        // act
        using var response = await Client.PutAsync($"/reports", JsonContent.Create(report));
        await Server.QueryDbAsync(async ctx => actual = await ctx.Reports.FindAsync(report.Id));

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        actual.ShouldNotBeNull().ShouldBeEquivalentTo(expected);
    }
}