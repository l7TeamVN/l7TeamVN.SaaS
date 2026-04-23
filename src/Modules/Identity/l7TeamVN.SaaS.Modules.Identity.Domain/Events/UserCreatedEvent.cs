using l7TeamVN.SaaS.Domain.Events;

namespace l7TeamVN.SaaS.Modules.Identity.Domain.Events;

public record UserCreatedEvent : IDomainEvent
{
    public Guid UserId { get; init; }
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
