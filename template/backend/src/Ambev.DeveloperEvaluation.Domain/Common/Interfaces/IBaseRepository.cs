using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
{
    Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void DeleteAll(IEnumerable<TEntity> entities);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    IQueryable<TEntity> Queryable(int pageNumber = 1, int pageSize = 100);

    IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate);

    IQueryable<TEntity> QueryableComplete(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>> predicate);

    IQueryable<TEntity> QueryableComplete(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity?> GetCompleteWithFilterAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}