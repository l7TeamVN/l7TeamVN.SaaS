using l7TeamVN.SaaS.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace l7TeamVN.SaaS.Modules.Identity.Persistence;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContextUnitOfWork<IdentityDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
