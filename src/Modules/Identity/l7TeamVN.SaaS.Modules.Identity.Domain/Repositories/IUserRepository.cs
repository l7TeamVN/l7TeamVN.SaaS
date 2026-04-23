using l7TeamVN.SaaS.Domain.Repositories;
using l7TeamVN.SaaS.Modules.Identity.Domain.Entities.Aggregates;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;

namespace l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(EmailAddress Email, CancellationToken cancellationToken = default);
}
