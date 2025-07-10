using System.Text;
using Ambev.DeveloperEvaluation.Packages.Security.ApplicationUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ambev.DeveloperEvaluation.Packages.Security;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .RegisterApplicationUserService()
            .RegisterAuthentication(configuration);
    }

    public static IServiceCollection RegisterApplicationAuthorization(this IServiceCollection services,
        Action<AuthorizationOptions> options)
    {
        return services.RegisterAuthorization(options);
    }

    public static IApplicationBuilder ConfigureApplicationSecurity(this IApplicationBuilder app)
    {
        return app
            .UseAuthentication()
            .UseAuthorization();
    }

    private static IServiceCollection RegisterApplicationUserService(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddScoped<IApplicationUserService, ApplicationUserService>();
    }

    private static IServiceCollection RegisterAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.RegisterDefaultAuthentication(configuration);
    }

    private static IServiceCollection RegisterDefaultAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:SecretKey"];
        ArgumentException.ThrowIfNullOrWhiteSpace(secretKey);

        var key = Encoding.ASCII.GetBytes(secretKey);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    private static IServiceCollection RegisterAuthorization(this IServiceCollection services,
        Action<AuthorizationOptions> options)
    {
        return services.AddAuthorization(options);
    }
}