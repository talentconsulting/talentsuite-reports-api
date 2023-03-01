using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

    public async Task InitialiseAsync(bool isProduction)
    {
        try
        {
            if (_context.Database.IsInMemory())
            {
                await _context.Database.EnsureDeletedAsync();
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
        if (_context.Projects.Any())
            return;

        _context.Clients.AddRange(Clients().ToArray());
        _context.ProjectRoles.AddRange(ProjectRoles().ToArray());
        _context.Projects.Add(new Project("86b610ee-e866-4749-9f10-4a5c59e96f2f", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31),
            new List<ClientProject>(),
            new List<Contact>(),
            new List<Report>(),
            new List<Sow>()));
        _context.UserGroups.AddRange(UserGroups().ToArray());
        _context.Users.AddRange(Users().ToArray());
        _context.Contacts.Add(new Contact("03a33a03-a98d-4946-8e8f-05cbc7a949b6", "Ron Weasley", "ron@weasley.com", true, "86b610ee-e866-4749-9f10-4a5c59e96f2f"));

        await _context.SaveChangesAsync();
    }

    private static List<Client> Clients() 
    {
        return new List<Client>
        {
            new Client("83c756a8-ff87-48be-a862-096678b41817", "Harry Potter", "DfE", "harry@potter.com" ),
            new Client("e24a5543-6368-490a-a1d0-a18f0c69848a", "Hermione Granger", "ESFA", "hermione@granger.com" )
        };
    }

    private static List<ProjectRole> ProjectRoles()
    {
        return new List<ProjectRole>
        {
            new ProjectRole("626bff24-61c7-49d3-81bd-4aa12311e103", "Developer", true, "Developer on the project writing the code" ),
            new ProjectRole("fe32f237-ce7e-48f2-add7-fa5dc725396c", "Architect", true, "Over seeing the architecture of the project" ),
            new ProjectRole("4d3e2d9e-53fe-4a98-9062-9353a54bdece", "Business Analyst", false, "Analysing business needs" ),
            new ProjectRole("ed939f0b-4793-4e7c-82fa-f621cb0d8785", "Architect", false, "Over seeing the architecture of the project" )
        };
    }

    private static List<UserGroup> UserGroups()
    {
        return new List<UserGroup>
        {
            new UserGroup("2a91939a-57fd-4049-afa9-88e547c5bd92", "Global Administrator", true),
            new UserGroup("3a38a77c-3bda-4950-8802-e1b636c4c29f", "Project Admin", true),
            new UserGroup("768aa546-ec03-4663-b7f4-26569932b2af", "User", false),
        };
    }

    private static List<User> Users()
    {
        return new List<User>
        {
            new User("93e0f88c-691f-4373-8abf-3f895bddec60", "Joe", "Blogs", "joe.blogs@email.com", "768aa546-ec03-4663-b7f4-26569932b2af", Reports().Where(x => x.UserId == "93e0f88c-691f-4373-8abf-3f895bddec60").ToList()),
            new User("8ed672f0-5146-4ecc-89a0-6a36c1f5db71", "John", "Brown", "john.brown@email.com", "768aa546-ec03-4663-b7f4-26569932b2af", Reports().Where(x => x.UserId == "8ed672f0-5146-4ecc-89a0-6a36c1f5db71").ToList()),
        };
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
                submissiondate: new DateTime(2023,4,1),
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
                submissiondate: new DateTime(2023,4,1),
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
