using l7TeamVN.SaaS.Persistence.Interceptors;

namespace Microsoft.Extensions.DependencyInjection;

public static class PersistenceServicesCollectionExtensions
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();
        return services;
    }
}
