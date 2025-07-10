using Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.ProductAggregate.Entities;

public class Product : AuditableEntity, IAggregateRoot
{
    public const int MaxLengthName = 100;
    public const int MaxLengthDescription = 500;
    public const int MaxLengthSku = 50;
    public const int MaxLengthBarcode = 50;

    protected Product() { }

    public Product(
        string name,
        string description,
        string sku,
        string barcode,
        Price price,
        ProductStatus status,
        int stockQuantity)
    {
        ValidateProduct(name, description, sku, barcode, stockQuantity);

        Name = name;
        Description = description;
        Sku = sku;
        Barcode = barcode;
        Price = price;
        Status = status;
        StockQuantity = stockQuantity;
    }

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Sku { get; private set; } = null!;
    public string Barcode { get; private set; } = null!;
    public Price Price { get; private set; } = null!;
    public ProductStatus Status { get; private set; }
    public int StockQuantity { get; private set; }

    public void Update(
        string name,
        string description,
        string sku,
        string barcode,
        Price price,
        ProductStatus status,
        int stockQuantity)
    {
        ValidateProduct(name, description, sku, barcode, stockQuantity);

        Name = name;
        Description = description;
        Sku = sku;
        Barcode = barcode;
        Price = price;
        Status = status;
        StockQuantity = stockQuantity;
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
        {
            throw new RuleViolationDomainException("Stock quantity cannot be negative");
        }

        StockQuantity = quantity;
    }

    public void Activate() => Status = ProductStatus.Active;
    public void Deactivate() => Status = ProductStatus.Inactive;
    public void Discontinue() => Status = ProductStatus.Discontinued;

    private static void ValidateProduct(
        string name,
        string description,
        string sku,
        string barcode,
        int stockQuantity)
    {
        if (name.Length > MaxLengthName)
        {
            throw new RuleViolationDomainException($"Name must be less than {MaxLengthName} characters.");
        }

        if (description.Length > MaxLengthDescription)
        {
            throw new RuleViolationDomainException($"Description must be less than {MaxLengthDescription} characters.");
        }

        if (sku.Length > MaxLengthSku)
        {
            throw new RuleViolationDomainException($"SKU must be less than {MaxLengthSku} characters.");
        }

        if (barcode.Length > MaxLengthBarcode)
        {
            throw new RuleViolationDomainException($"Barcode must be less than {MaxLengthBarcode} characters.");
        }

        if (stockQuantity < 0)
        {
            throw new RuleViolationDomainException("Stock quantity cannot be negative");
        }
    }
}