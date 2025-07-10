using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF;

public static class DependencyInjection
{
    internal static void AddEfCore(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<IUnitOfWork, DefaultContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole(x =>
            {
                x.FormatterName = environment.IsDevelopment() ? "simple" : "json";
            }))).EnableSensitiveDataLogging();
        });
    }
}