using Serilog;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.ReportsApi;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    Log.Information("Reporting API initialising");
    var builder = WebApplication.CreateBuilder(args);
    builder.Configure();

    var app = builder.Build();
    await app.Configure();
    await app.RunAsync();
}
catch (HostAbortedException ex)
{
    Log.Fatal(ex, "Host has been aborted by the framework");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
}
finally
{
    await Log.CloseAndFlushAsync();
}

[ExcludeFromCodeCoverage]
public partial class Program { }