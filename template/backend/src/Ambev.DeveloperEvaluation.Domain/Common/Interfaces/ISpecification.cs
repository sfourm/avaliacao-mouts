namespace Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

public interface ISpecification<TEntity> where TEntity : BaseEntity
{
    bool IsSatisfiedBy(TEntity entity);
}