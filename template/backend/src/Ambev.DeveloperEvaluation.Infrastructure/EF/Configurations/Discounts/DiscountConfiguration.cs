using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Configurations.Discounts;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("discounts");

        // Configure properties
        builder.Property(d => d.Name)
            .HasColumnName("name")
            .HasColumnType($"character varying({Discount.MaxLengthName})")
            .HasMaxLength(Discount.MaxLengthName)
            .IsRequired();

        builder.Property(d => d.Description)
            .HasColumnName("description")
            .HasColumnType($"character varying({Discount.MaxLengthDescription})")
            .HasMaxLength(Discount.MaxLengthDescription)
            .IsRequired();

        builder.Property(d => d.Code)
            .HasColumnName("code")
            .HasColumnType($"character varying({Discount.MaxLengthCode})")
            .HasMaxLength(Discount.MaxLengthCode)
            .IsRequired();

        builder.Property(d => d.Value)
            .HasColumnName("value")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Configure DiscountType enum
        builder.Property(d => d.Type)
            .HasColumnName("type")
            .HasConversion<string>()
            .HasColumnType("character varying(20)")
            .IsRequired();

        // Configure dates
        builder.Property(d => d.StartDt)
            .HasColumnName("start_date")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(d => d.EndDt)
            .HasColumnName("end_date")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Configure boolean flags
        builder.Property(d => d.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(d => d.IsPublic)
            .HasColumnName("is_public")
            .HasColumnType("boolean")
            .HasDefaultValue(false);

        // Configure optional numeric fields
        builder.Property(d => d.MaxUses)
            .HasColumnName("max_uses")
            .HasColumnType("integer");

        builder.Property(d => d.MinOrderValue)
            .HasColumnName("min_order_value")
            .HasColumnType("integer");

        // Configure indexes
        builder.HasIndex(d => d.Code).IsUnique();
        builder.HasIndex(d => d.Name);
        builder.HasIndex(d => d.IsActive);
        builder.HasIndex(d => d.IsPublic);

        builder.ConfigureAuditableEntity();
    }
}