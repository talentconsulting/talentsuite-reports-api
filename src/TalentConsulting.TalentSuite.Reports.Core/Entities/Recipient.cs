using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Recipient : EntityBase<string>, IAggregateRoot
{
    private Recipient() { }

    public Recipient(string id, string name, string email, int notificationid)
    {
        Id = id;
        Name = name;
        Email = email;
        Notificationid = notificationid;
    }

    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int Notificationid { get; set; } = default!;

}