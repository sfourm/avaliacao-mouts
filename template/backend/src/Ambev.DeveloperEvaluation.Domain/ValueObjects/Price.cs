using System.Globalization;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Price : ValueObject
{
    public const string DefaultCurrency = "BRL";
    public const int MaxCurrencyLength = 3;

    public Price(decimal amount, string currency = DefaultCurrency)
    {
        if (amount < 0)
            throw new RuleViolationDomainException("Amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new RuleViolationDomainException("Currency cannot be empty.");

        if (currency.Length != MaxCurrencyLength)
            throw new RuleViolationDomainException($"Currency must be {MaxCurrencyLength} characters long.");

        Amount = amount;
        Currency = currency.ToUpper();
    }

    protected Price() { }

    public decimal Amount { get; }
    public string Currency { get; } = null!;

    public static Price operator +(Price a, Price b)
    {
        ValidateSameCurrency(a, b);
        return new Price(a.Amount + b.Amount, a.Currency);
    }

    public static Price operator -(Price a, Price b)
    {
        ValidateSameCurrency(a, b);
        return new Price(a.Amount - b.Amount, a.Currency);
    }

    private static void ValidateSameCurrency(Price a, Price b)
    {
        if (a.Currency != b.Currency)
            throw new RuleViolationDomainException("Cannot perform operations with different currencies.");
    }

    public override string ToString()
    {
        return $"{Amount.ToString("N2", CultureInfo.InvariantCulture)} {Currency}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public static implicit operator decimal(Price price) => price.Amount;
    public static implicit operator Price(decimal amount) => new(amount);
}