using System.Reflection.Metadata;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record UserDto
{
    private UserDto() { }

    public UserDto(string id, string firstname, string lastname, string email, int usergroupid)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        UserGroupId = usergroupid;
    }

    public string Id { get; init; } = default!;
    public string Firstname { get; init; } = default!;
    public string Lastname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public int UserGroupId { get; init; } = default!;
}