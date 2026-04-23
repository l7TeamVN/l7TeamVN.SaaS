using l7TeamVN.SaaS.Application.Messaging;

namespace l7TeamVN.SaaS.Modules.Identity.Application.Authentication.Register;

public record RegisterUserCommand(string UserName, string Email, string Password) : ICommand<Guid>;