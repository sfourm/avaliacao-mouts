using Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.DiscountAggregate.Entities;

public class Discount : AuditableEntity, IAggregateRoot
{
    public const int MaxLengthName = 100;
    public const int MaxLengthDescription = 255;
    public const int MaxLengthCode = 50;

    public Discount(
        string name,
        string description,
        string code,
        decimal value,
        DiscountType type,
        DateTime startDt,
        DateTime endDt,
        bool isActive,
        int? maxUses = null,
        int? minOrderValue = null,
        bool? isPublic = false)
    {
        ValidateDiscount(name, description, code, startDt, endDt);
        
        Name = name;
        Description = description;
        Code = code;
        Value = value;
        Type = type;
        StartDt = startDt;
        EndDt = endDt;
        IsActive = isActive;
        MaxUses = maxUses;
        MinOrderValue = minOrderValue;
        IsPublic = isPublic;
    }

    protected Discount()
    {
    }

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public decimal Value { get; private set; }
    public DiscountType Type { get; private set; }
    public DateTime StartDt { get; private set; }
    public DateTime EndDt { get; private set; }
    public bool IsActive { get; private set; }
    public int? MaxUses { get; private set; } = 1;
    public int? MinOrderValue { get; private set; }
    public bool? IsPublic { get; private set; }
    
    public void Update(
        string name,
        string description,
        string code,
        decimal value,
        DiscountType type,
        DateTime startDt,
        DateTime endDt,
        bool isActive,
        int? maxUses = null,
        int? minOrderValue = null,
        bool? isPublic = false)
    {
        ValidateDiscount(name, description, code, startDt, endDt);
        
        Name = name;
        Description = description;
        Code = code;
        Value = value;
        Type = type;
        StartDt = startDt;
        EndDt = endDt;
        IsActive = isActive;
        MaxUses = maxUses;
        MinOrderValue = minOrderValue;
        IsPublic = isPublic;
    }

    private static void ValidateDiscount(
        string name,
        string description,
        string code,
        DateTime startDt,
        DateTime endDt)
    {
        if (name.Length > MaxLengthName)
        {
            throw new RuleViolationDomainException($"Name must be less than {MaxLengthName} characters.");
        }

        if (description.Length > MaxLengthDescription)
        {
            throw new RuleViolationDomainException($"Description must be less than {MaxLengthDescription} characters.");
        }

        if (code.Length > MaxLengthCode)
        {
            throw new RuleViolationDomainException($"Code must be less than {MaxLengthCode} characters.");
        }

        if (endDt <= startDt)
        {
            throw new RuleViolationDomainException("End date must be after start date");
        }
    }

    public void ValidateDiscountUsage(decimal totalPrice)
    {
        if (!IsActive)
        {
            throw new RuleViolationDomainException("Discount is not active");
        }

        if (DateTime.UtcNow < StartDt)
        {
            throw new RuleViolationDomainException($"Discount will only be valid from {StartDt:MM/dd/yyyy}");
        }

        if (DateTime.UtcNow > EndDt)
        {
            throw new RuleViolationDomainException("Discount has expired");
        }

        if (MaxUses.HasValue)
        {
            throw new RuleViolationDomainException("Discount usage limit has been reached");
        }

        if (totalPrice < MinOrderValue)
        {
            throw new RuleViolationDomainException(
                $"Minimum order value to use this discount is {MinOrderValue.Value:C}");
        }
    }
}