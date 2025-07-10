using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides methods for generating test data using the Bogus library.
///     This class centralizes all test data generation to ensure consistency
///     across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UserTestData
{
    private static readonly Faker<User> UserFaker = new Faker<User>()
        .CustomInstantiator(f => new User(
            new Name(f.Person.FirstName, f.Person.LastName),
            new Email(f.Internet.Email()),
            new Phone("55", f.Phone.PhoneNumber("###########")),
            new Username(f.Internet.UserName()),
            new Password($"Test@{f.Random.Number(100, 999)}"),
            f.PickRandom(UserRole.Customer, UserRole.Admin),
            f.PickRandom(UserStatus.Active, UserStatus.Suspended, UserStatus.Inactive)
        ))
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended));

    /// <summary>
    ///     Generates a valid User entity with randomized data.
    /// </summary>
    public static User GenerateValidUser()
    {
        return UserFaker.Generate();
    }

    /// <summary>
    ///     Generates a valid Name value object.
    /// </summary>
    public static Name GenerateValidName()
    {
        return new Name(new Faker().Person.FirstName, new Faker().Person.LastName);
    }

    /// <summary>
    ///     Generates a valid email address.
    /// </summary>
    public static Email GenerateValidEmail()
    {
        return new Email(new Faker().Internet.Email());
    }

    /// <summary>
    ///     Generates a valid Brazilian phone number.
    /// </summary>
    public static Phone GenerateValidPhone()
    {
        return new Phone("55", new Faker().Phone.PhoneNumber("###########"));
    }

    /// <summary>
    ///     Generates a valid username.
    /// </summary>
    public static Username GenerateValidUsername()
    {
        return new Username(new Faker().Internet.UserName());
    }

    /// <summary>
    ///     Generates a valid password meeting complexity requirements.
    /// </summary>
    public static Password GenerateValidPassword()
    {
        return new Password($"Test@{new Faker().Random.Number(100, 999)}");
    }

    /// <summary>
    ///     Generates an invalid email address (missing @ symbol).
    /// </summary>
    public static string GenerateInvalidEmailString()
    {
        return new Faker().Lorem.Word();
    }

    /// <summary>
    ///     Generates an invalid password (too short).
    /// </summary>
    public static string GenerateInvalidPasswordString()
    {
        return new Faker().Random.String2(5);
    }

    /// <summary>
    ///     Generates an invalid phone number (wrong format).
    /// </summary>
    public static string GenerateInvalidPhoneString()
    {
        return new Faker().Random.AlphaNumeric(5);
    }

    /// <summary>
    ///     Generates a username that exceeds maximum length.
    /// </summary>
    public static string GenerateLongUsernameString()
    {
        return new Faker().Random.String2(Username.MaxLength + 1);
    }

    /// <summary>
    ///     Generates a first name that exceeds maximum length.
    /// </summary>
    public static string GenerateLongFirstNameString()
    {
        return new Faker().Random.String2(Name.MaxFirstNameLength + 1);
    }

    /// <summary>
    ///     Generates a last name that exceeds maximum length.
    /// </summary>
    public static string GenerateLongLastNameString()
    {
        return new Faker().Random.String2(Name.MaxLastNameLength + 1);
    }
}