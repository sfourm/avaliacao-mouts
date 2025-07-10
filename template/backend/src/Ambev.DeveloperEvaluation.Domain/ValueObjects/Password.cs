namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Password : ValueObject
{
    public const int MinLength = 8;
    public const int MaxLength = 100;
    private static readonly Regex SpecialCharsRegex = new(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new RuleViolationDomainException("Password cannot be empty or whitespace.");

        ValidatePasswordRules(value);
        Value = value;
    }

    protected Password()
    {
    }

    public string Value { get; } = null!;

    public static void ValidatePasswordRules(string password)
    {
        switch (password.Length)
        {
            case < MinLength:
                throw new RuleViolationDomainException(
                    $"Password must be at least {MinLength} characters long.");
            case > MaxLength:
                throw new RuleViolationDomainException(
                    $"Password must be at most {MaxLength} characters long.");
        }

        if (!password.Any(char.IsDigit))
            throw new RuleViolationDomainException(
                "Password must contain at least one digit.");

        if (!password.Any(char.IsLower))
            throw new RuleViolationDomainException(
                "Password must contain at least one lowercase letter.");

        if (!password.Any(char.IsUpper))
            throw new RuleViolationDomainException(
                "Password must contain at least one uppercase letter.");

        if (!SpecialCharsRegex.IsMatch(password))
            throw new RuleViolationDomainException(
                "Password must contain at least one special character.");
    }

    public static implicit operator string(Password password)
    {
        return password.Value;
    }

    public static implicit operator Password(string password)
    {
        return new Password(password);
    }

    public override string ToString()
    {
        return "********";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}