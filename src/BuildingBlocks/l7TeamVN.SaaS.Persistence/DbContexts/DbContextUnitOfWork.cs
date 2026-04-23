using l7TeamVN.SaaS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace l7TeamVN.SaaS.Persistence.DbContexts;

public abstract class DbContextUnitOfWork<TDbContext>(DbContextOptions<TDbContext> options) : DbContext(options), IUnitOfWork where TDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
