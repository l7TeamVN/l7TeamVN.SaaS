using l7TeamVN.SaaS.Modules.Identity.Application.Contracts;
using l7TeamVN.SaaS.Modules.Identity.Infrastructure.Authentication.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityServicesCollectionExtensions
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.Configure<JwtOptions>(options =>
        {
            options.SecretKey = jwtOptions.SecretKey;
            options.Issuer = jwtOptions.Issuer;
            options.Audience = jwtOptions.Audience;
            options.ExpiryMinutes = jwtOptions.ExpiryMinutes;
        });

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddJwtAuthentication(jwtOptions);
        return services;
    }



    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            };
        });

        return services;
    }
}
