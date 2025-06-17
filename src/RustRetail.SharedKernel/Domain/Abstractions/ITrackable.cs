namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for entities that track creation and update timestamps.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTimeOffset? CreatedDateTime { get; }

        /// <summary>
        /// Gets the date and time when the entity was last updated.
        /// </summary>
        DateTimeOffset? UpdatedDateTime { get; }

        /// <summary>
        /// Sets the creation date and time.
        /// </summary>
        /// <param name="createdDateTime">The creation timestamp to set.</param>
        void SetCreatedDateTime(DateTimeOffset? createdDateTime);

        /// <summary>
        /// Sets the last updated date and time.
        /// </summary>
        /// <param name="updatedDateTime">The update timestamp to set.</param>
        void SetUpdatedDateTime(DateTimeOffset? updatedDateTime);
    }
}
