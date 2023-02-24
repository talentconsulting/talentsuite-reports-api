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
        _context.Projects.Add(new Project("86b610ee-e866-4749-9f10-4a5c59e96f2f", "0121 111 2222", "Social work CPD", "con_23sds", new DateTime(2023, 10, 01), new DateTime(2023, 03, 31)));
        _context.UserGroups.AddRange(UserGroups().ToArray());
        _context.Contacts.Add(new Contact("03a33a03-a98d-4946-8e8f-05cbc7a949b6", "Ron Weasley", "ron@weasley.com", true, "86b610ee-e866-4749-9f10-4a5c59e96f2f"));

        await _context.SaveChangesAsync();
    }

    private List<Client> Clients() 
    {
        return new List<Client>
        {
            new Client("83c756a8-ff87-48be-a862-096678b41817", "Harry Potter", "DfE", "harry@potter.com" ),
            new Client("e24a5543-6368-490a-a1d0-a18f0c69848a", "Hermione Granger", "ESFA", "hermione@granger.com" )
        };
    }

    private List<ProjectRole> ProjectRoles()
    {
        return new List<ProjectRole>
        {
            new ProjectRole("626bff24-61c7-49d3-81bd-4aa12311e103", "Developer", true, "Developer on the project writing the code" ),
            new ProjectRole("fe32f237-ce7e-48f2-add7-fa5dc725396c", "Architect", true, "Over seeing the architecture of the project" ),
            new ProjectRole("4d3e2d9e-53fe-4a98-9062-9353a54bdece", "Business Analyst", false, "Analysing business needs" ),
            new ProjectRole("ed939f0b-4793-4e7c-82fa-f621cb0d8785", "Architect", false, "Over seeing the architecture of the project" )
        };
    }

    private List<UserGroup> UserGroups()
    {
        return new List<UserGroup>
        {
            new UserGroup("2a91939a-57fd-4049-afa9-88e547c5bd92", "Global Administrator", true),
            new UserGroup("3a38a77c-3bda-4950-8802-e1b636c4c29f", "Project Admin", true),
            new UserGroup("768aa546-ec03-4663-b7f4-26569932b2af", "User", false),
        };
    }
}
