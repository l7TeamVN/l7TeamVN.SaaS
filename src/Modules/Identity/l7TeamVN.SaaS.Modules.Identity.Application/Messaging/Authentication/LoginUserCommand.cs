using FluentValidation;
using l7TeamVN.SaaS.Application.Contracts;
using l7TeamVN.SaaS.Application.Messaging;
using l7TeamVN.SaaS.Application.Messaging.Handlers;
using l7TeamVN.SaaS.Modules.Identity.Application.Contracts;
using l7TeamVN.SaaS.Modules.Identity.Application.Dtos;
using l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;
using l7TeamVN.SaaS.SharedKernel.Results;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;

namespace l7TeamVN.SaaS.Modules.Identity.Application.Messaging.Authentication;

public record LoginUserCommand : ICommand<AuthDto>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}

public class LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) : ICommandHandler<LoginUserCommand, AuthDto>
{
    public async Task<Result<AuthDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var resultEmail = EmailAddress.Create(request.Email);
        if (resultEmail.IsFailure)
        {
            return Result.Failure<AuthDto>(resultEmail.Error);
        }
        var user = await userRepository.GetByEmailAsync(resultEmail.Value, cancellationToken);
        bool isPasswordValid = passwordHasher.Verify(request.Password!, user.PasswordHash);

        if (user == null || !isPasswordValid)
        {
            return Result.Failure<AuthDto>(new Error("Auth.Login", "Invalid credentials"));
        }

        var token = jwtProvider.GenerateToken(user.Id, user.Email);

        var authDto = new AuthDto
        {
            AccessToken = token
        };

        return Result.Success(authDto);
    }
}
