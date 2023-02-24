using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Recipient : EntityBase<string>, IAggregateRoot
{
    private Recipient() { }

    public Recipient(string id, string name, string email, string notificationid)
    {
        Id = id;
        Name = name;
        Email = email;
        Notificationid = notificationid;
    }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Notificationid { get; set; } = null!;
#if ADD_ENTITY_NAV
    public virtual Notification Notification { get; set; } = null!;
#endif


}