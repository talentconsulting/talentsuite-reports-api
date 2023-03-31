using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Audit : EntityBase<string>, IAggregateRoot
{
    private Audit() { }

    public Audit(string id, string detail, string userid)
    {
        base.Id = id;
        Detail = detail;
        UserId = userid;
    }

    public string Detail { get; init; } = null!;
    public string UserId { get; init; } = null!;
}