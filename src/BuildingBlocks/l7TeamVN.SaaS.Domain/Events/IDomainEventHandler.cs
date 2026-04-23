using MediatR;

namespace l7TeamVN.SaaS.Domain.Events;

public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
{
}