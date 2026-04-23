using l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;
using l7TeamVN.SaaS.Modules.Identity.Persistence;
using l7TeamVN.SaaS.Modules.Identity.Persistence.Repositories;
using l7TeamVN.SaaS.SharedKernel.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityServicesCollectionExtensions
{
    public static IServiceCollection AddIdentityPersistence(this IServiceCollection services, ConnectionStringsOptions connectionStrings)
    {

        services.AddSqlServerDbContext<IdentityDbContext>(connectionStrings);

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
