using l7TeamVN.SaaS.Domain.Entities;
using l7TeamVN.SaaS.Modules.Identity.Domain.Events;
using l7TeamVN.SaaS.SharedKernel.Results;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;

namespace l7TeamVN.SaaS.Modules.Identity.Domain.Entities.Aggregates;

public class User : AggregateRoot<Guid>
{
    public string UserName { get; private set; } = "None";
    public EmailAddress Email { get; private set; } = EmailAddress.Empty;
    public string? FullName { get; private set; }
    public WebsiteUrl? AvatarUrl { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; private set; }
    public DateTimeOffset? LastLoginAt { get; private set; }


    private User() { }

    public static Result<User> Create(string userName, string email, string passwordHash, string? phoneNumber = null)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return Result.Failure<User>(new Error("User.EmptyUserName", "Name user not empty."));
        }

        var emailResult = EmailAddress.Create(email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        var phoneResult = PhoneNumber.CreateOrNull(phoneNumber);
        if (phoneResult.IsFailure)
        {
            return Result.Failure<User>(phoneResult.Error);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = emailResult.Value,
            PhoneNumber = phoneResult.Value,
            PasswordHash = passwordHash,
        };

        user.AddDomainEvent(new UserCreatedEvent {
            UserId = user.Id
        });

        return user;
    }
}
