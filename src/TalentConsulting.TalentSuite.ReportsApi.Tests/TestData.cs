using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

internal class TestData
{
    public static class Client1
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly Guid Project1Id = Guid.NewGuid();
        public static readonly Guid Project2Id = Guid.NewGuid();
        public static readonly Guid Sow1Id = Guid.NewGuid();

        public static readonly ICollection<Report> Reports = [
            new Report() {
                Id = Guid.NewGuid(),
                ClientId = Id,
                ProjectId = Project1Id,
                SowId = Sow1Id,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = Guid.NewGuid(),
                        Description = "Green risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Green,
                    }
                ]
            },
            new Report() {
                Id = Guid.NewGuid(),
                ClientId = Id,
                ProjectId = Project1Id,
                SowId = Sow1Id,
                Status = ReportStatus.Saved,
                Risks = [
                    new Risk() {
                        Id = Guid.NewGuid(),
                        Description = "Amber risk description",
                        Mitigation = string.Empty,
                        Status = RiskStatus.Amber,
                    }
                ]
            }
        ];
    }



    
}
