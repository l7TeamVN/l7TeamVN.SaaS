using l7TeamVN.SaaS.Modules.Identity.ConfigurationOptions;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityServicesCollectionExtensions
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, Action<IdentityModuleOptions> configureOptions)
    {
        var settings = new IdentityModuleOptions();
        configureOptions(settings);

        services.Configure(configureOptions);


        services.AddIdentityApplication();
        services.AddIdentityInfrastructure();
        services.AddIdentityPersistence(settings.ConnectionStrings);
        return services;
    }
}
