using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;
using TalentConsulting.TalentSuite.Reports.Core;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Interceptors;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.UnitTests;

public class BaseCreateDbUnitTest
{
    protected static Guid _projectId = new Guid("a3226044-5c89-4257-8b07-f29745a22e2c");
    protected static Guid _reportId = new Guid("5698dbc0-a10c-43e5-9074-4ce6d6637778");
    protected static Guid _userId = new Guid("ce6edc11-3477-4b88-946d-598d5a7aa68a");
    protected static Guid _riskId = new Guid("41fef4ce-c85f-4273-8572-0222e471db63");
    protected const string _clientId = "1e68f5cd-2347-4b09-820e-3297605e3743";
    protected const string _usergroupId = "2a91939a-57fd-4049-afa9-88e547c5bd92";
    protected const string _clientProjectId = "519df403-0e0d-4c25-b240-8d9ca21132b8";

    protected IMapper _mapper { get; }

    protected BaseCreateDbUnitTest()
    {
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(myProfile);
        });
        _mapper = new Mapper(configuration);
    }

    protected ApplicationDbContext GetApplicationDbContext()
    {
        var options = CreateNewContextOptions();
#if USE_DISPATCHER
        var mockEventDispatcher = new Mock<IDomainEventDispatcher>();
#endif
        var mockDateTime = new Mock<IDateTime>();
        var mockCurrentUserService = new Mock<ICurrentUserService>();
        var auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(mockCurrentUserService.Object, mockDateTime.Object);
        var mockApplicationDbContext = new ApplicationDbContext(options,
#if USE_DISPATCHER
            mockEventDispatcher.Object, 
#endif
            auditableEntitySaveChangesInterceptor);

        return mockApplicationDbContext;
    }

    protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase("ClientProjectsDb")
               .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}
