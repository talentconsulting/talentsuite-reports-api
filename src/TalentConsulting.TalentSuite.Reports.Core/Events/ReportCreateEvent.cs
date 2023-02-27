using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Events;

public interface IReportCreatedEvent
{
    Report Item { get; }
}

public class ReportCreatedEvent : DomainEventBase, IReportCreatedEvent
{
    public ReportCreatedEvent(Report item)
    {
        Item = item;
    }

    public Report Item { get; }
}
