using System.Reflection.Metadata;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record UserGroupDto
{
    private UserGroupDto() { }

    public UserGroupDto(string id, string name, bool receivesreports)
    {
        Id = id;
        Name = name;
        ReceivesReports = receivesreports;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public bool ReceivesReports { get; init; } = default!;

}