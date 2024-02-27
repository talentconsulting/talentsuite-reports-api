using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("projectroles")]
public class ProjectRole : IAggregateRoot
{
    private ProjectRole() { }

    public ProjectRole(string id, string name, bool technical, string description)
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
        Technical = technical;
        Description = description;
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public bool Technical { get; set; }
    public string Description { get; set; } = null!;
}
