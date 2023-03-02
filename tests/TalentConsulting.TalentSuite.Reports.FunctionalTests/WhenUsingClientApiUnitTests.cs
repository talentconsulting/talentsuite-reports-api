using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TalentConsulting.TalentSuite.Reports.FunctionalTests;



[Collection("Sequential")]
public class WhenUsingClientsApiUnitTests : BaseWhenUsingApiUnitTests
{
#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenClientsAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/clients?pageNumber=1&pageSize=10"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ClientDto>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(0);
    }

#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenTheClientIsCreated()
    {
        var report = GetTestClientDto(Guid.NewGuid().ToString());

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/clients"),
            Content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif


        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        stringResult.Should().Be(report.Id);
    }

#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenTheClientIsUpdated()
    {
        var id = Guid.NewGuid().ToString();
        var report = GetTestClientDto(id);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/clients"),
            Content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var updatedreport = new ClientDto(id, "Name 1", "Contact Name 1", "Contact Email 1", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) });

        var updaterequest = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_client.BaseAddress + $"api/clients/{updatedreport.Id}"),
            Content = new StringContent(JsonConvert.SerializeObject(updatedreport), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif

        using var updateresponse = await _client.SendAsync(updaterequest);

        updateresponse.EnsureSuccessStatusCode();

        var updateStringResult = await updateresponse.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updateStringResult.Should().Be(updatedreport.Id);
    }

    public static ClientDto GetTestClientDto(string clientId)
    {
        return new ClientDto(clientId, "Name", "Contact Name", "Contact Email", new List<ClientProjectDto>() { new ClientProjectDto(_clientProjectId, _clientId, _projectId) });
    }
}
