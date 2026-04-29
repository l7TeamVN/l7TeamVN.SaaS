using l7TeamVN.SaaS.Modules.Identity.Infrastructure.Authentication.Jwt;
using l7TeamVN.SaaS.SharedKernel.Options;

namespace l7TeamVN.SaaS.Modules.Identity.ConfigurationOptions;

public class IdentityModuleOptions
{
    public ConnectionStringsOptions? ConnectionStrings { get; set; }

    public JwtOptions? JwtOptions { get; set; }
}
