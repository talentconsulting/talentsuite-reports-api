using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("notifications")]
public class Notification : EntityBase<string>, IAggregateRoot
{
    private Notification() { }

    public Notification(string id, string content, DateTime nextretrydate, string title, string status)
    {
        base.Id = id;
        NextRetryDate = nextretrydate;
        Content = content;
        Title = title;
        Status = status;
    }

    public DateTime? NextRetryDate { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Status { get; init; } = null!;
    public virtual ICollection<Recipient> Recipients { get; } = new List<Recipient>();
}

