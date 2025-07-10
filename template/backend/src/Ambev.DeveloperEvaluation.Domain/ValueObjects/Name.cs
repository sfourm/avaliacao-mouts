namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Name : ValueObject
{
    public const int MaxFirstNameLength = 99;
    public const int MaxLastNameLength = 99;

    public Name(string firstName, string lastName)
    {
        if (firstName.Length >= MaxFirstNameLength)
            throw new RuleViolationDomainException(
                $"The {nameof(firstName)} must be smaller or equal to {MaxFirstNameLength}.");

        if (lastName.Length >= MaxLastNameLength)
            throw new RuleViolationDomainException(
                $"The {nameof(lastName)} must be smaller or equal to {MaxLastNameLength}.");

        FirstName = firstName;
        LastName = lastName;
    }

    protected Name()
    {
    }

    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }

    public static implicit operator string(Name name)
    {
        return name.GetFullName();
    }

    public static implicit operator Name(string name)
    {
        var data = name.Split(" ");
        return data.Length != 2
            ? throw new RuleViolationDomainException(
                "Input string was not in a correct format. It must be composed by two words separated by space.")
            : new Name(data[0], data[1]);
    }

    public override string ToString()
    {
        return GetFullName();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}