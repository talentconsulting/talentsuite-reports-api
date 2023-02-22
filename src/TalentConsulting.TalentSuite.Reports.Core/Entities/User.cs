using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class User : EntityBase<string>, IAggregateRoot
{
    private User() { }

    public User(string id, string firstname, string lastname, string email, bool receivesreports, int usergroupid)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        ReceivesReports = receivesreports;
        UserGroupId = usergroupid;
    }

    public string Firstname { get; init; } = default!;
    public string Lastname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReports { get; init; } = default!;
    public int UserGroupId { get; init; } = default!;
}