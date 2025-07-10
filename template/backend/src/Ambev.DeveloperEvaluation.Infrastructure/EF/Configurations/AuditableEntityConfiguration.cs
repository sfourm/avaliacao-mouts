using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Configurations;

public static class AuditableEntityConfiguration
{
    public static void ConfigureAuditableEntity<TBuilder>(this EntityTypeBuilder<TBuilder> builder)
        where TBuilder : AuditableEntity, IEntity
    {
        builder.ConfigureBaseEntity();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasIndex(p => p.CreatedAt);
        builder.HasIndex(p => p.UpdatedAt);
    }
}