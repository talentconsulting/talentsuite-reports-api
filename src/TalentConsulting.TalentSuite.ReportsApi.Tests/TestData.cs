using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class TestData
{
    public static class Client1
    {
        public static readonly Guid Id = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid ProjectId = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid SowId = TestContext.CurrentContext.Random.NextGuid();

        public static readonly ICollection<Report> Reports = [
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = ProjectId,
                SowId = SowId,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Green risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Green,
                    }
                ]
            },
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = ProjectId,
                SowId = SowId,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Amber risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Amber,
                    }
                ]
            },
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = ProjectId,
                SowId = SowId,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Amber risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Amber,
                    }
                ]
            },
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = ProjectId,
                SowId = SowId,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Red risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Red,
                    }
                ]
            },
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = ProjectId,
                SowId = SowId,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Amber risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Amber,
                    }
                ]
            }
        ];

        internal static IEnumerable<Report> GenerateNewReports(int count)
        {
            return TestData.GenerateNewReports(count, Id, ProjectId, SowId);
        }
    }

    public static class Client2
    {
        public static readonly Guid Id = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid ProjectId = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid SowId = TestContext.CurrentContext.Random.NextGuid();

        internal static IEnumerable<Report> GenerateNewReports(int count)
        {
            return TestData.GenerateNewReports(count, Id, ProjectId, SowId);
        }
    }

    private static IEnumerable<Report> GenerateNewReports(int count, Guid clientId, Guid projectId, Guid sowId)
    {
        return Enumerable.Range(1, count).Select(x => new Report()
        {
            Id = TestContext.CurrentContext.Random.NextGuid(),
            ClientId = clientId,
            ProjectId = projectId,
            SowId = sowId,
            Status = ReportStatus.Submitted,
            Risks = [
                new Risk() {
                        Id = TestContext.CurrentContext.Random.NextGuid(),
                        Description = "Amber risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Amber,
                    }
            ]
        });
    }
}
