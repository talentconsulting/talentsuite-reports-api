using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Project : EntityBase<string>, IAggregateRoot
{
    private Project() { }

    public Project(string id, string clId, string name, string reference, DateTime startDate, DateTime endDate)
    {
        Id = id;
        ClId = clId;
        Name = name;
        Reference = reference;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string ClId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
}
