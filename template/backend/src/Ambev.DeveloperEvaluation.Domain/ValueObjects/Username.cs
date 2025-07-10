namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Username : ValueObject
{
    public const int MinLength = 4;
    public const int MaxLength = 20;
    private static readonly Regex ValidCharactersRegex = new(@"^[a-zA-Z0-9_\-\.]+$");

    public Username(string value)
    {
        IsValidUsername(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Username username)
    {
        return username.Value;
    }

    public static implicit operator Username(string username)
    {
        return new Username(username);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool IsValidUsername(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new RuleViolationDomainException("Username cannot be empty or whitespace.");

        switch (value.Length)
        {
            case < MinLength:
                throw new RuleViolationDomainException(
                    $"Username must be at least {MinLength} characters long.");
            case > MaxLength:
                throw new RuleViolationDomainException(
                    $"Username must be at most {MaxLength} characters long.");
        }

        if (!ValidCharactersRegex.IsMatch(value))
            throw new RuleViolationDomainException(
                "Username can only contain letters, numbers, underscores, hyphens, and periods.");

        return true;
    }
}