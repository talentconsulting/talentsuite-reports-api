using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class User : EntityBase<string>, IAggregateRoot
{
    private User() { }

    public User(string id, string firstname, string lastname, string email, string usergroupid)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        UserGroupId = usergroupid;
    }

    public string Firstname { get; init; } = null!;
    public string Lastname { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string UserGroupId { get; init; } = default!;
    public virtual ICollection<Report> Reports { get; } = new List<Report>();
#if ADD_ENTITY_NAV
    public virtual UserGroup Usergroup { get; set; } = null!;
#endif

}