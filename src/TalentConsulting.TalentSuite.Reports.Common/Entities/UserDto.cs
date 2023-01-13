using System.Reflection.Metadata;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record UserDto
{
    private UserDto() { }

    public UserDto(string id, string firstname, string lastname, string email, bool receivesreports, int usergroupid)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        ReceivesReports = receivesreports;
        UserGroupId = usergroupid;
    }

    public string Id { get; init; } = default!;
    public string Firstname { get; init; } = default!;
    public string Lastname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReports { get; init; } = default!;
    public int UserGroupId { get; init; } = default!;
}