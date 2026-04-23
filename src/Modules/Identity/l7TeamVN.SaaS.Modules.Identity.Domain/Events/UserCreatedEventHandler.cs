using l7TeamVN.SaaS.Domain.Events;
using Microsoft.Extensions.Logging;

namespace l7TeamVN.SaaS.Modules.Identity.Domain.Events;

public class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger) : IDomainEventHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger = logger;
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User created: {UserId}", notification.UserId);
    }
}
