using MediatR;

namespace l7TeamVN.SaaS.Domain.Events;

public interface IDomainEvent : INotification
{
    DateTimeOffset OccurredOn { get; }
}
