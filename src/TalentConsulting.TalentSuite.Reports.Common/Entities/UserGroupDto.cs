using System.Reflection.Metadata;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record UserGroupDto
{
    private UserGroupDto() { }

    public UserGroupDto(string id, string name, bool receivesreports, ICollection<UserDto> users)
    {
        Id = id;
        Name = name;
        ReceivesReports = receivesreports;
        Users = users;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public bool ReceivesReports { get; init; } = default!;
    public ICollection<UserDto> Users { get; init;  } = new List<UserDto>();

}