using System.ComponentModel.DataAnnotations.Schema;

namespace TalentConsulting.TalentSuite.Reports.Common;

/// <summary>
/// Base types for all Entities which track state using a given Id.
/// </summary>
public abstract class EntityBase<Tid>
{
    public string id { get; set; } = default!;

    public DateTime? created { get; set; } = default;

    private readonly List<DomainEventBase> _domainEvents = new();

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    internal void ClearDomainEvents() => _domainEvents.Clear();
}

