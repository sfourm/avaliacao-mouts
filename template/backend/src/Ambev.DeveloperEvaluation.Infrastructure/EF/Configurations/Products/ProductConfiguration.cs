using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.EF.Configurations.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType($"varchar({Product.MaxLengthName})")
            .HasMaxLength(Product.MaxLengthName)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType($"varchar({Product.MaxLengthDescription})")
            .HasMaxLength(Product.MaxLengthDescription)
            .IsRequired();

        builder.Property(p => p.Sku)
            .HasColumnName("sku")
            .HasColumnType($"varchar({Product.MaxLengthSku})")
            .HasMaxLength(Product.MaxLengthSku)
            .IsRequired();

        builder.Property(p => p.Barcode)
            .HasColumnName("barcode")
            .HasColumnType($"varchar({Product.MaxLengthBarcode})")
            .HasMaxLength(Product.MaxLengthBarcode)
            .IsRequired();

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasColumnType("varchar(3)")
                .IsRequired();
        });

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(p => p.StockQuantity)
            .HasColumnName("stock_quantity")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(p => p.Sku).IsUnique();
        builder.HasIndex(p => p.Barcode).IsUnique();
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Status);

        builder.ConfigureAuditableEntity();
    }
}