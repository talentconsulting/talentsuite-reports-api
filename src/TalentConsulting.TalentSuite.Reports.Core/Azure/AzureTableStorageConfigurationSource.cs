using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace TalentConsulting.TalentSuite.Reports.Core.Azure;

public class AzureTableStorageConfigurationSource : IConfigurationSource
{
    internal readonly IEnumerable<string> ConfigurationKeys;
    internal readonly IEnumerable<string> ConfigurationKeysRawJsonResult;
    internal readonly string ConnectionString;
    internal readonly string EnvironmentName;
    internal readonly bool PrefixConfigurationKeys;


    public AzureTableStorageConfigurationSource(ConfigurationOptions configOptions)
    {
        ConnectionString = configOptions.TableStorageConnectionString;
        EnvironmentName = configOptions.EnvironmentName;
        ConfigurationKeys = configOptions.ConfigurationKeys;
        PrefixConfigurationKeys = configOptions.PrefixConfigurationKeys;
        ConfigurationKeysRawJsonResult = configOptions.ConfigurationKeysRawJsonResult;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AzureTableStorageConfigurationProvider(CloudStorageAccount.Parse(ConnectionString), EnvironmentName,
            ConfigurationKeys, PrefixConfigurationKeys, ConfigurationKeysRawJsonResult);
    }
}