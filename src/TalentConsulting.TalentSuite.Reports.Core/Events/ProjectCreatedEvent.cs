using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Events;

public interface IProjectCreatedEvent
{
    Project Item { get; }
}

public class ProjectCreatedEvent : DomainEventBase, IProjectCreatedEvent
{
    public ProjectCreatedEvent(Project item)
    {
        Item = item;
    }

    public Project Item { get; }
}

