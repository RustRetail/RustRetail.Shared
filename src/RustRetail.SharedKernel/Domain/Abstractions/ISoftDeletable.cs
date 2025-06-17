namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for entities that support soft deletion.
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is soft deleted.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was soft deleted.
        /// </summary>
        DateTimeOffset? DeletedDateTime { get; set; }
    }
}
