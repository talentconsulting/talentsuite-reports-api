using FluentAssertions;
using System.Net;
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

        var r1 = await response.Content.ReadAsStringAsync();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReportDto>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(1);
    }

}
