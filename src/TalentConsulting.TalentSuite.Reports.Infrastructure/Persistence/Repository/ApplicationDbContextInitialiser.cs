using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync(bool isProduction, bool restartDatabase)
    {
        try
        {
            if (restartDatabase)
            {
                await _context.Database.EnsureDeletedAsync();
            }

            if (_context.Database.IsInMemory())
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();
            }

            if (_context.Database.IsSqlite())
            {
                await _context.Database.EnsureCreatedAsync();
            }

            if (_context.Database.IsSqlServer() || _context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (_context.Reports.Any())
            return;

        if (_context.Database.IsInMemory())
        {
            _context.Reports.AddRange(Reports());
        }


        await _context.SaveChangesAsync();
    }

    private static List<Report> Reports()
    {
        return new List<Report>
        {
            new Report(
                id:"b112342a-8bfc-4a37-97af-04b53e2cf48e",
                plannedtasks: "Task 2, Task 3",
                completedtasks: "Task 1",
                weeknumber: 1,
                submissiondate: new DateTime(2023,4,1, 0,0,0, DateTimeKind.Utc),
                projectid: "86b610ee-e866-4749-9f10-4a5c59e96f2f",
                userid: "93e0f88c-691f-4373-8abf-3f895bddec60", new List<Risk>()
                {
                    new Risk(id: "f211de16-dfde-451f-b63c-56099c79adf6", 
                    reportid: "b112342a-8bfc-4a37-97af-04b53e2cf48e", 
                    riskdetails: "Risk Details 1", 
                    riskmitigation: "Risk Mitigation 1", 
                    ragstatus: "Rag Status 1")
                }),

            new Report(
                id:"47084b7a-0d7a-462d-ab9f-5c0bbb4e70bc",
                plannedtasks: "Task 2, Task 3",
                completedtasks: "Task 1",
                weeknumber: 1,
                submissiondate: new DateTime(2023,4,1, 0,0,0, DateTimeKind.Utc),
                projectid: "86b610ee-e866-4749-9f10-4a5c59e96f2f",
                userid: "8ed672f0-5146-4ecc-89a0-6a36c1f5db71", new List<Risk>()
                {
                    new Risk(id: "c3bfb9e1-58e5-4d9f-bd75-1e386a2b7480",
                    reportid: "47084b7a-0d7a-462d-ab9f-5c0bbb4e70bc",
                    riskdetails: "Risk Details 2",
                    riskmitigation: "Risk Mitigation 2",
                    ragstatus: "Rag Status 2")
                })
        };
    }
}
