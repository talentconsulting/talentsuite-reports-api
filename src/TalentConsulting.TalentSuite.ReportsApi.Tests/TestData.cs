using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class TestData
{
    public static class Client1
    {
        public static readonly Guid Id = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid Project1Id = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid Project2Id = TestContext.CurrentContext.Random.NextGuid();
        public static readonly Guid Sow1Id = TestContext.CurrentContext.Random.NextGuid();

        public static readonly ICollection<Report> Reports = [
            new Report() {
                Id = TestContext.CurrentContext.Random.NextGuid(),
                ClientId = Id,
                ProjectId = Project1Id,
                SowId = Sow1Id,
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
                ProjectId = Project1Id,
                SowId = Sow1Id,
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
    }
}
