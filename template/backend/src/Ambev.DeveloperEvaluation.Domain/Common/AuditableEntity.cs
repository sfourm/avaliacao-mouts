namespace Ambev.DeveloperEvaluation.Domain.Common;

public abstract class AuditableEntity : BaseEntity, IAuditableEntity
{
    protected AuditableEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public void Modify()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}