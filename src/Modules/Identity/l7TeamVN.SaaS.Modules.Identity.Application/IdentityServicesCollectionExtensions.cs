using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityServicesCollectionExtensions
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        services.AddMessageHandler(Assembly.GetExecutingAssembly());
        return services;
    }
}
