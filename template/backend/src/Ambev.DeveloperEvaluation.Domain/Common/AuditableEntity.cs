using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public abstract class AuditableEntity : IAuditableEntity
{
    protected AuditableEntity()
    {
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
    }

    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }

    public void Modify()
    {
        Updated = DateTime.UtcNow;
    }
}