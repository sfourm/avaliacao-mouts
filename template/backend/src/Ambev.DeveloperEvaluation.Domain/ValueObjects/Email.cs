using System.Net.Mail;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Email : ValueObject
{
    public const int MaxEmailLength = 100;

    public Email(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        if (value.Length > MaxEmailLength)
            throw new RuleViolationDomainException(
                $"The provided e-mail must be smaller or equal to {MaxEmailLength}.");

        if (!MailAddress.TryCreate(value, out _))
            throw new RuleViolationDomainException("The provided email address is not valid.");

        Address = value.ToLower();
    }

    protected Email()
    {
    }

    public string Address { get; } = null!;

    public static implicit operator string(Email email)
    {
        return email.Address.ToLower();
    }

    public static implicit operator Email(string address)
    {
        return new Email(address);
    }

    public override string ToString()
    {
        return Address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}