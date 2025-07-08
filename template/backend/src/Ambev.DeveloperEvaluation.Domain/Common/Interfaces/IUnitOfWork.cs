namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}