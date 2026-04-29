namespace l7TeamVN.SaaS.Modules.Identity.Application.Contracts;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string email);
}
