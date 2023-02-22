using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Audit : EntityBase<string>, IAggregateRoot
{
    private Audit() { }

    public Audit(string id, DateTime created, string detail, string userid)
    {
        Id = id;
        CreatedDate = created;
        Detail = detail;
        UserId = userid;
    }

    public string Id { get; init; } = default!;
    public DateTime CreatedDate { get; init; } = default!;
    public string Detail { get; init; } = default!;
    public string UserId { get; init; } = default!;
}