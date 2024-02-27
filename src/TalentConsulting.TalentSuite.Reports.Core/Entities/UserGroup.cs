using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("usergroups")]
public class UserGroup : IAggregateRoot
{
    private UserGroup() { }

    public UserGroup(string id, string name, bool receivesreports)
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
        ReceivesReports = receivesreports;
    }

    public Guid Id { get; set; }
    public string Name { get; init; } = null!;
    public bool? ReceivesReports { get; init; } = null!;
    public virtual ICollection<User> Users { get; } = new List<User>();

}