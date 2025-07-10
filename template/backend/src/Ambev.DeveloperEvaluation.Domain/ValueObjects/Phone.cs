namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Phone : ValueObject
{
    public const int MinIddLength = 1;
    public const int MaxIddLength = 4;
    public const int MaxNumberLength = 15;
    public const char MinIddValue = '0';

    protected Phone()
    {
    }

    public Phone(string idd, string number)
    {
        EnsureIddIsValid(idd);
        EnsureNumberIsValid(number);

        Idd = idd;
        Number = number;
    }

    public string Idd { get; } = null!;
    public string Number { get; } = null!;

    public string GetFullPhone()
    {
        return $"+{Idd} {Number}";
    }

    public static implicit operator Phone(string phone)
    {
        var data = phone.Split(" ");

        if (data.Length != 2)
            throw new RuleViolationDomainException(
                "Input string was not in a correct format. It must be composed by two words separated by space.");

        return new Phone(data[0], data[1]);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Idd;
        yield return Number;
    }

    private static void EnsureIddIsValid(string idd)
    {
        if (string.IsNullOrWhiteSpace(idd)) throw new RuleViolationDomainException("You must inform a IDD.");

        if (!idd.All(char.IsDigit))
            throw new RuleViolationDomainException($"The {nameof(idd)} must contain only digits.");

        switch (idd.Length)
        {
            case < MinIddLength:
                throw new RuleViolationDomainException($"The {nameof(idd)} must be greater than {MinIddLength}.");
            case MinIddLength when idd[0] == MinIddValue:
                throw new RuleViolationDomainException($"The {nameof(idd)} must not be zero.");
            case > MaxIddLength:
                throw new RuleViolationDomainException($"The {nameof(idd)} must be less or equal to {MaxIddLength}.");
        }
    }

    private static void EnsureNumberIsValid(string number)
    {
        if (string.IsNullOrWhiteSpace(number)) throw new RuleViolationDomainException("You must inform a number.");

        if (!number.All(char.IsDigit))
            throw new RuleViolationDomainException($"The {nameof(number)} must contain only digits.");

        if (number.Length > MaxNumberLength)
            throw new RuleViolationDomainException($"The {nameof(number)} must be less or equal to {MaxNumberLength}.");
    }

    public static implicit operator string(Phone phone)
    {
        return phone.GetFullPhone();
    }

    public override string ToString()
    {
        return GetFullPhone();
    }
}