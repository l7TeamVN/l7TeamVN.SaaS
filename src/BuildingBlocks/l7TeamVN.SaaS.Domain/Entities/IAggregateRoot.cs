using l7TeamVN.SaaS.Domain.Events;

namespace l7TeamVN.SaaS.Domain.Entities;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
