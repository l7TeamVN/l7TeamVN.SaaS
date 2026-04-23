using l7TeamVN.SaaS.Domain.Events;
using l7TeamVN.SaaS.Modules.Identity.Domain.Events;

namespace l7TeamVN.SaaS.Modules.Identity.Application.Authentication.Register;

public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
    }
}