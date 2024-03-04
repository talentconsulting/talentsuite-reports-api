namespace TalentConsulting.TalentSuite.Reports.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBaseEx<string>> entitiesWithEvents);
}
