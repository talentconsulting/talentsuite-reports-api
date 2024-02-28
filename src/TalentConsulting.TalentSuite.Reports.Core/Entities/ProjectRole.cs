using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("projectroles")]
public class ProjectRole : EntityBase<Guid>, IAggregateRoot
{
    private ProjectRole() { }

    public ProjectRole(Guid id, string name, bool technical, string description)
    {
        Id = id;
        Name = name;
        Technical = technical;
        Description = description;
    }

    public string Name { get; set; } = null!;
    public bool Technical { get; set; }
    public string Description { get; set; } = null!;
}
