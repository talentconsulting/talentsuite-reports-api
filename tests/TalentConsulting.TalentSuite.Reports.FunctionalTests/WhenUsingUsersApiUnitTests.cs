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
public class WhenUsingUsersApiUnitTests : BaseWhenUsingApiUnitTests
{
#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenUsersAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/users?pageNumber=1&pageSize=10"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<UserDto>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(1);
    }

#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenTheProjectIsCreated()
    {
        var user = GetTestUserDto(Guid.NewGuid().ToString());


        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/users"),
            Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif


        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        stringResult.Should().Be(user.Id);
    }

#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenTheReportIsUpdated()
    {
        var id = Guid.NewGuid().ToString();
        var user = GetTestUserDto(id);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/users"),
            Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var updateduser = new UserDto(id, "First Name1", "Last Name1", "email1@email.com", _usergroupId, new List<ReportDto>() { WhenUsingReportsApiUnitTests.GetTestReportDto(_reportId) });
       
        var updaterequest = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_client.BaseAddress + $"api/users/{updateduser.Id}"),
            Content = new StringContent(JsonConvert.SerializeObject(updateduser), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif

        using var updateresponse = await _client.SendAsync(updaterequest);

        updateresponse.EnsureSuccessStatusCode();

        var updateStringResult = await updateresponse.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updateStringResult.Should().Be(updateduser.Id);
    }

    public static UserDto GetTestUserDto(string userId)
    {
        return new UserDto(userId, "First Name", "Last Name", "email@email.com", _usergroupId, new List<ReportDto>() { WhenUsingReportsApiUnitTests.GetTestReportDto(_reportId) });
    }
}
