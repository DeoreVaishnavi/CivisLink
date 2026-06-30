namespace CivicHero.Backend.Core.Common;

/// <summary>
/// Base class for entities that require audit information.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the UTC date and time when the entity was created.
    /// </summary>
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    public Guid? CreatedBy { get; protected set; }

    /// <summary>
    /// Gets or sets the UTC date and time when the entity was last modified.
    /// </summary>
    public DateTime? LastModifiedOnUtc { get; protected set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// </summary>
    public Guid? LastModifiedBy { get; protected set; }

    /// <summary>
    /// Marks the entity as created.
    /// </summary>
    /// <param name="userId">The identifier of the creating user.</param>
    public void MarkAsCreated(Guid? userId)
    {
        CreatedOnUtc = DateTime.UtcNow;
        CreatedBy = userId;
    }

    /// <summary>
    /// Marks the entity as modified.
    /// </summary>
    /// <param name="userId">The identifier of the modifying user.</param>
    public void MarkAsModified(Guid? userId)
    {
        LastModifiedOnUtc = DateTime.UtcNow;
        LastModifiedBy = userId;
    }
}