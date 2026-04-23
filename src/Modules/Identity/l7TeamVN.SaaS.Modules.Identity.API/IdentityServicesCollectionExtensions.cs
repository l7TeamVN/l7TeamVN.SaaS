using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityServicesCollectionExtensions
{
    public static IMvcBuilder AddIdentityAPI(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(Assembly.GetExecutingAssembly());
    }
}
