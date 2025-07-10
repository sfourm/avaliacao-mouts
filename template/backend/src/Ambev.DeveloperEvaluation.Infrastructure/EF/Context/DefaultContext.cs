using System.Reflection;
using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Context;

public sealed class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Product> Products => Set<Product>();

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}