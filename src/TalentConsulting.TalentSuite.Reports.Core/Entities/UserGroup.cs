using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class UserGroup : EntityBase<string>, IAggregateRoot
{
    private UserGroup() { }

    public UserGroup(string id, string name, bool receivesreports)
    {
        Id = id;
        Name = name;
        ReceivesReports = receivesreports;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public bool ReceivesReports { get; init; } = default!;

}