using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TalentConsulting.TalentSuite.Reports.API;
using TalentConsulting.TalentSuite.Reports.Core.Entities;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Reports.FunctionalTests;

public abstract class BaseWhenUsingApiUnitTests : IDisposable
{
    protected readonly HttpClient? _client;
    protected readonly JwtSecurityToken? _token;
    protected readonly MyWebApplicationFactory? _webAppFactory;
    private readonly IConfiguration? _configuration;
    private bool _disposed;
    private readonly bool _initSuccessful;

    protected const string _projectId = "a3226044-5c89-4257-8b07-f29745a22e2c";
    protected const string _reportId = "5698dbc0-a10c-43e5-9074-4ce6d6637778";
    protected const string _userId = "ce6edc11-3477-4b88-946d-598d5a7aa68a";
    protected const string _riskId = "41fef4ce-c85f-4273-8572-0222e471db63";
    protected const string _clientId = "1e68f5cd-2347-4b09-820e-3297605e3743";
    protected const string _usergroupId = "2a91939a-57fd-4049-afa9-88e547c5bd92";
    protected const string _clientProjectId = "519df403-0e0d-4c25-b240-8d9ca21132b8";

    protected BaseWhenUsingApiUnitTests()
    {
        _disposed = false;

        try
        {
            var config = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .Build();

            _configuration = config;

            //var config = new ConfigurationBuilder()
            //.AddInMemoryCollection(new List<KeyValuePair<string, string?>>()
            //{
            //    new KeyValuePair<string, string?>("UseDbType", "UseInMemoryDatabase"),
            //    new KeyValuePair<string, string?>("JWT:Secret", "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr")
            //})
            //.Build();

            List<Claim> authClaims = new List<Claim> { new Claim(ClaimTypes.Role, "TalentConsultingUser") };
            _token = CreateToken(authClaims, config);

            _webAppFactory = new MyWebApplicationFactory();

            _client = _webAppFactory.CreateDefaultClient();
            _client.BaseAddress = new Uri("https://localhost:7055/");

            _initSuccessful = true;
        }
        catch
        {
            _initSuccessful = false;
        }

    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims, IConfiguration configuration)
    {
        var secret = configuration["JWT:Secret"] ?? "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr";
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        if (!int.TryParse(configuration["JWT:TokenValidityInMinutes"], out var tokenValidityInMinutes))
        {
            tokenValidityInMinutes = 30;
        }

        var token = new JwtSecurityToken(
            configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    protected virtual void Dispose(bool disposing)
    {
        // Cleanup
        if (!_disposed && disposing)
        {
            Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        if (!_initSuccessful)
        {
            return;
        }

        if (_webAppFactory != null)
        {
            using var scope = _webAppFactory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

        if (_client != null)
        {
            _client.Dispose();
        }

        if (_webAppFactory != null)
        {
            _webAppFactory.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    protected bool IsRunningLocally()
    {

        if (!_initSuccessful || _configuration == null)
        {
            return false;
        }

        try
        {
            string localMachineName = _configuration["LocalSettings:MachineName"] ?? string.Empty;

            if (!string.IsNullOrEmpty(localMachineName))
            {
                return Environment.MachineName.Equals(localMachineName, StringComparison.OrdinalIgnoreCase);
            }
        }
        catch
        {
            return false;
        }

        // Fallback to a default check if User Secrets file or machine name is not specified
        // For example, you can add additional checks or default behavior here
        return false;
    }
}
