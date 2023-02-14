using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Notification : EntityBase<string>, IAggregateRoot
{
    private Notification() { }

    public Notification(string id, DateTime created, String content, DateTime nextretrydate, string title, int status)
    {
        Id = id;
        Created = created;
        NextRetryDate = nextretrydate;
        Content = content;
        Title = title;
        Status = status;
    }

    public DateTime NextRetryDate { get; init; } = default!;
    public string Content { get; init; } = default!;
    public string Title { get; init; } = default!;
    public int Status { get; init; } = default!;
}

