namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    void Modify();
}