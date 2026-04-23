using l7TeamVN.SaaS.Persistence.Interceptors;
using l7TeamVN.SaaS.SharedKernel.Options;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlServerServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerDbContext<TContext>(this IServiceCollection services, ConnectionStringsOptions configureOptions) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            var domainEventsInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptor>();

            options.UseSqlServer(configureOptions.Default, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(configureOptions.MigrationsAssembly);
                if (configureOptions.CommandTimeout.HasValue)
                {
                    sqlOptions.CommandTimeout(configureOptions.CommandTimeout.Value);
                }
            });

            options.AddInterceptors(auditableInterceptor, domainEventsInterceptor);
        });

        return services;
    }
}
