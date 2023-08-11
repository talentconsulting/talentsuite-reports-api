namespace TalentConsulting.TalentSuite.Reports.Core.Azure;

public class ConfigurationOptions
{
    public string EnvironmentName { get; set; }
    public string TableStorageConnectionString { get; set; }
    public string[] ConfigurationKeys { get; set; }
    public bool PrefixConfigurationKeys { get; set; } = true;
    public string[] ConfigurationKeysRawJsonResult { get; set; } = null;
}