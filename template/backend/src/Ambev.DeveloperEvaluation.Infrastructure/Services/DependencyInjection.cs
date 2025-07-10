using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Ambev.DeveloperEvaluation.Infrastructure.Services.Auth;
using Ambev.DeveloperEvaluation.Infrastructure.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Infrastructure.Services;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IPhoneService, PhoneService>();
    }
}