namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    DateTime Created { get; }
    DateTime? Updated { get; }
    void Modify();
}