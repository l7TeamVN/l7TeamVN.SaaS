using FluentValidation;
using l7TeamVN.SaaS.Application.Contracts;
using l7TeamVN.SaaS.Application.Messaging;
using l7TeamVN.SaaS.Application.Messaging.Handlers;
using l7TeamVN.SaaS.Modules.Identity.Domain.Entities.Aggregates;
using l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;
using l7TeamVN.SaaS.SharedKernel.Results;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;

namespace l7TeamVN.SaaS.Modules.Identity.Application.Messaging.Authentication;

public record RegisterUserCommand : ICommand<Guid>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}

public class RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var resultEmail = EmailAddress.Create(request.Email);
        if (resultEmail.IsFailure)
        {
            return Result.Failure<Guid>(resultEmail.Error);
        }
        var existingUser = await userRepository.GetByEmailAsync(resultEmail.Value, cancellationToken);
        if (existingUser != null)
        {
            return Result.Failure<Guid>(new Error("User.EmailAlreadyExists", "Email is already registered."));
        }

        var hashedPassword = passwordHasher.Hash(request.Password);
        var result = User.Create(request.UserName, request.Email, hashedPassword);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var newUser = result.Value;

        await userRepository.CreateAsync(newUser, cancellationToken);

        return newUser.Id;
    }
}
