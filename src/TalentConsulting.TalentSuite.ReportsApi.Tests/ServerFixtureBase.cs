namespace TalentConsulting.TalentSuite.ReportsApi.Tests;

public abstract class ServerFixtureBase
{
    private TestServer _server;
    private HttpClient _client;

    internal TestServer Server => _server;
    internal HttpClient Client => _client;

    [SetUp]
    public virtual Task BaseSetup()
    {
        _server = new TestServer();
        _client = _server.CreateClient();

        return Task.CompletedTask;
    }

    [TearDown]
    public virtual async Task BaseTearDown()
    {
        _client.Dispose();
        await _server.DisposeAsync();
    }
}