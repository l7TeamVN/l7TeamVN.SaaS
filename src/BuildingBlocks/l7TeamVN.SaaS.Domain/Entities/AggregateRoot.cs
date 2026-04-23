using l7TeamVN.SaaS.Domain.Events;

namespace l7TeamVN.SaaS.Domain.Entities;

public abstract class AggregateRoot<T> : AuditableEntity<T>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
