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
public class WhenUsingReportsApiUnitTests : BaseWhenUsingApiUnitTests
{
#if DEBUG
    [Fact]
#else
    [Fact(Skip = "This test should be run locally")]
#endif
    public async Task ThenReportsAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + "api/reports?pageNumber=1&pageSize=10"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReportDto>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
    public async Task ThenTheReportIsCreated()
    {
        var report = GetTestReportDto(Guid.NewGuid().ToString());

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/reports"),
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
    public async Task ThenTheReportIsUpdated()
    {
        var id = Guid.NewGuid().ToString();
        var report = GetTestReportDto(id);
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/reports"),
            Content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json"),
        };

#if ADD_BEARER_TOKEN
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");
#endif

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync();

        var updatedreport = new ReportDto(id, DateTime.UtcNow.AddDays(-1), "Planned tasks1", "Completed tasks1", 1, DateTime.UtcNow, _projectId, _userId, new List<RiskDto>()
        {
            new RiskDto(_riskId, _reportId, "Risk Details 1", "Risk Mitigation1", "Risk Status1" )
        });

        var updaterequest = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(_client.BaseAddress + $"api/reports/{updatedreport.Id}"),
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

    public static ReportDto GetTestReportDto(string reportId)
    {
        var risks = new List<RiskDto>()
        {
            new RiskDto(_riskId, _reportId, "Risk Details", "Risk Mitigation", "Risk Status" )
        };

        return new ReportDto(reportId, DateTime.UtcNow.AddDays(-1), "Planned tasks", "Completed tasks", 1, DateTime.UtcNow, _projectId, _userId, risks);
    }

}
