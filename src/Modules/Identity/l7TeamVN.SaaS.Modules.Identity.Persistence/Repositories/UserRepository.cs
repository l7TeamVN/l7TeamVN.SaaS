using l7TeamVN.SaaS.Modules.Identity.Domain.Entities.Aggregates;
using l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;
using l7TeamVN.SaaS.Persistence.Repositories;
using l7TeamVN.SaaS.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace l7TeamVN.SaaS.Modules.Identity.Persistence.Repositories;

public class UserRepository(IdentityDbContext dbContext) : GenericRepository<IdentityDbContext, User, Guid>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmailAsync(EmailAddress Email, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Email == Email, cancellationToken);
    }
}
