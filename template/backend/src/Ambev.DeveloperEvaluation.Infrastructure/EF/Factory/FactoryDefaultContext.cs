using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Factory;

public class FactoryDefaultContext : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        DbContextOptionsBuilder<DefaultContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString,
            options => { options.MigrationsAssembly(typeof(DefaultContext).Assembly.FullName); });

        return new DefaultContext(optionsBuilder.Options);
    }
}