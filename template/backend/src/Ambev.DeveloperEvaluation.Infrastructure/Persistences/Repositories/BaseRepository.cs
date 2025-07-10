using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Ambev.DeveloperEvaluation.Infrastructure.EF.Context;
using Ambev.DeveloperEvaluation.Infrastructure.Persistences.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
{
    private readonly DbSet<TEntity> _dbSet;
    protected readonly DefaultContext Context;

    public BaseRepository(DefaultContext dbContext)
    {
        Context = dbContext;
        _dbSet = Context.Set<TEntity>();
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken);
        if (entity != null) _dbSet.Remove(entity);
    }

    public virtual IQueryable<TEntity> Queryable(int pageNumber = 1, int pageSize = 100)
    {
        return _dbSet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet
            .AsNoTracking()
            .Where(predicate);
    }

    public virtual void DeleteAll(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetCompleteWithFilterAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbSet
            .IncludeWithFilter(predicate)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual IQueryable<TEntity> QueryableComplete(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet
            .IncludeWithFilter(predicate)
            .AsSplitQuery()
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public virtual IQueryable<TEntity> QueryableComplete(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet
            .IncludeWithFilter(predicate)
            .AsSplitQuery()
            .AsNoTracking();
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(x => x.Id == id, cancellationToken);
    }
}