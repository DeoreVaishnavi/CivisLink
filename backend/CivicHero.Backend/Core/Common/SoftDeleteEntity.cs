namespace CivicHero.Backend.Core.Common;

/// <summary>
/// Base class for entities that support soft deletion.
/// Instead of permanently removing records from the database,
/// entities are marked as deleted and can be restored later.
/// </summary>
public abstract class SoftDeleteEntity : AuditableEntity
{
    /// <summary>
    /// Gets a value indicating whether the entity has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Gets the UTC date and time when the entity was deleted.
    /// </summary>
    public DateTime? DeletedOnUtc { get; private set; }

    /// <summary>
    /// Gets the identifier of the user who deleted the entity.
    /// </summary>
    public Guid? DeletedBy { get; private set; }

    /// <summary>
    /// Marks the entity as deleted.
    /// </summary>
    /// <param name="userId">
    /// The identifier of the user performing the deletion.
    /// </param>
    public void MarkAsDeleted(Guid? userId)
    {
        if (IsDeleted)
        {
            return;
        }

        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
        DeletedBy = userId;
    }

    /// <summary>
    /// Restores a previously soft-deleted entity.
    /// </summary>
    public void Restore()
    {
        IsDeleted = false;
        DeletedOnUtc = null;
        DeletedBy = null;
    }
}