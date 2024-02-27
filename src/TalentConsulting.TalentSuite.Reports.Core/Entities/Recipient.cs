using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("recipients")]
public class Recipient : IAggregateRoot
{
    private Recipient() { }

    public Recipient(string id, string name, string email, string notificationid)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        Name = name;
        Email = email;
        Notificationid = notificationid;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Notificationid { get; set; } = null!;
#if ADD_ENTITY_NAV
    public virtual Notification Notification { get; set; } = null!;
#endif


}