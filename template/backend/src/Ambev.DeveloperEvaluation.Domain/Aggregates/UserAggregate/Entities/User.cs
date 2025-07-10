using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;

public class User : AuditableEntity, IUser, IAggregateRoot
{
    protected User()
    {
    }

    public User(Name name, Email email, Phone phone, Username username, Password password, UserRole role,
        UserStatus status)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Username = username;
        Password = password;
        Role = role;
        Status = status;
    }

    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public UserStatus Status { get; private set; }
    public Username Username { get;  private set; } = null!;
    public UserRole Role { get;  private set; }

    public void Activate()
    {
        Status = UserStatus.Active;
    }

    public void Deactivate()
    {
        Status = UserStatus.Inactive;
    }

    public void Suspend()
    {
        Status = UserStatus.Suspended;
    }

    public void SetPassword(string password)
    {
        Password = password;
    }

    public bool IsSatisfiedBy()
    {
        return Status == UserStatus.Active;
    }
}