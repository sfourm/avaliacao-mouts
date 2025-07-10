using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Configurations;

public static class EntityConfiguration
{
    public static void ConfigureBaseEntity<TBuilder>(this EntityTypeBuilder<TBuilder> builder)
        where TBuilder : BaseEntity
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
    }
}