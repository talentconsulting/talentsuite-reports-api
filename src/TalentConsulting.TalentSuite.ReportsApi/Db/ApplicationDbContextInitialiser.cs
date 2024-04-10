using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;


namespace TalentConsulting.TalentSuite.ReportsApi.Db;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
{
    public async Task InitialiseAsync(bool isProduction, bool restartDatabase)
    {
        if (isProduction) return;

        try
        {
            if (restartDatabase)
            {
                await context.Database.EnsureDeletedAsync();
            }

            await InitializeDatabaseAsync();
            // Only await MigrateDatabaseAsync(); when using migrations directly
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private async Task InitializeDatabaseAsync()
    {
        if (context.Database.IsInMemory())
        {
            await context.Database.EnsureCreatedAsync();
        }
    }

    //Use this if using Migrations
    //private async Task MigrateDatabaseAsync()
    //{
    //    if (_context.Database.IsSqlServer() || _context.Database.IsNpgsql())
    //    {

    //        await _context.Database.MigrateAsync();

    //    }
    //}

    public async Task SeedAsync()
    {
        try
        {
            if (!context.Reports.Any())
            {
                await SeedReportsAsync();
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedReportsAsync()
    {
        var reports = Reports();
        context.Reports.AddRange(reports);
        await context.SaveChangesAsync();
    }

    private static List<Report> Reports()
    {
        return new List<Report>
        {
            new() {
                Id = new Guid("b112342a-8bfc-4a37-97af-04b53e2cf48e"),
                ClientId = new Guid("09A8D50B-B235-4D56-BB2B-1C90095B3806"),
                ProjectId = new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
                SowId = new Guid("BD23EB38-CC67-4978-88CC-19F196F91CB1"),
                Risks = new List<Risk>
                {
                    new ()
                    {
                        Id = new Guid("45B24D84-C4BD-4ED8-BFBC-C75E06AA3921"),
                        Description = "A description",
                        Mitigation = "",
                        Status = RiskStatus.Amber
                    }
                },
                Status = ReportStatus.Saved,
            },

            new() {
                Id = new Guid("F9E2BE52-7942-4D89-AFE1-A11B2E91C592"),
                ClientId = new Guid("09A8D50B-B235-4D56-BB2B-1C90095B3806"),
                ProjectId = new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
                SowId = new Guid("BD23EB38-CC67-4978-88CC-19F196F91CB1"),
                Risks = new List<Risk>
                {
                    new ()
                    {
                        Id = new Guid("498888BB-F790-43C5-9F0C-F0919AA62028"),
                        Description = "A description",
                        Mitigation = "",
                        Status = RiskStatus.Amber
                    }
                },
                Status = ReportStatus.Saved,
            },

            new() {
                Id = new Guid("EFB244FB-5D40-4C74-9DF2-D5514B730EF2"),
                ClientId = new Guid("09A8D50B-B235-4D56-BB2B-1C90095B3806"),
                ProjectId = new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
                SowId = new Guid("BD23EB38-CC67-4978-88CC-19F196F91CB1"),
                Risks = new List<Risk>
                {
                    new ()
                    {
                        Id = new Guid("A279D911-1616-4C69-90C5-87795A236E65"),
                        Description = "A description",
                        Mitigation = "",
                        Status = RiskStatus.Amber
                    }
                },
                Status = ReportStatus.Saved,
            }
            //new Report(
            //    id: new Guid("b112342a-8bfc-4a37-97af-04b53e2cf48e"),
            //    plannedtasks: "Task 2, Task 3",
            //    completedtasks: "Task 1",
            //    weeknumber: 1,
            //    submissiondate: new DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc),
            //    projectid: new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
            //    userid: new Guid("93e0f88c-691f-4373-8abf-3f895bddec60"),
            //    risks: new List<Risk>
            //    {
            //        new Risk(
            //            id: new Guid("f211de16-dfde-451f-b63c-56099c79adf6"),
            //            reportid: new Guid("b112342a-8bfc-4a37-97af-04b53e2cf48e"),
            //            riskdetails: "Risk Details 1",
            //            riskmitigation: "Risk Mitigation 1",
            //            ragstatus: "Rag Status 1"
            //        )
            //    }),
            
            //new Report(
            //    id: new Guid("187AAA69-0F95-48A5-908E-D5073D1B9CA7"),
            //    plannedtasks: "Task 2, Task 3",
            //    completedtasks: "Task 1",
            //    weeknumber: 1,
            //    submissiondate: new DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc),
            //    projectid: new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
            //    userid: new Guid("93e0f88c-691f-4373-8abf-3f895bddec60"),
            //    risks: new List<Risk>
            //    {
            //        new Risk(
            //            id: new Guid("5AC1B208-6B47-4AEF-9E0F-1D9E687C9AC6"),
            //            reportid: new Guid("187AAA69-0F95-48A5-908E-D5073D1B9CA7"),
            //            riskdetails: "Risk Details 1",
            //            riskmitigation: "Risk Mitigation 1",
            //            ragstatus: "Rag Status 1"
            //        )
            //    }),

            //new Report(
            //    id: new Guid("47084b7a-0d7a-462d-ab9f-5c0bbb4e70bc"),
            //    plannedtasks: "Task 2, Task 3",
            //    completedtasks: "Task 1",
            //    weeknumber: 1,
            //    submissiondate: new DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc),
            //    projectid: new Guid("86b610ee-e866-4749-9f10-4a5c59e96f2f"),
            //    userid: new Guid("8ed672f0-5146-4ecc-89a0-6a36c1f5db71"),
            //    risks: new List<Risk>
            //    {
            //        new Risk(
            //            id: new Guid("c3bfb9e1-58e5-4d9f-bd75-1e386a2b7480"),
            //            reportid: new Guid("47084b7a-0d7a-462d-ab9f-5c0bbb4e70bc"),
            //            riskdetails: "Risk Details 2",
            //            riskmitigation: "Risk Mitigation 2",
            //            ragstatus: "Rag Status 2"
            //        )
            //    })
        };
    }
}