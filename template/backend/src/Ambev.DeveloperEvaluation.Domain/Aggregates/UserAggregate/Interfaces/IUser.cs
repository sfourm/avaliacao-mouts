using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;

public interface IUser
{
    public Guid Id { get; }
    public Username Username { get; }
    public UserRole Role { get; }
}