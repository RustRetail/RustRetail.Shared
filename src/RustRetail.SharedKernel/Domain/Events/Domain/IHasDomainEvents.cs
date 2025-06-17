namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    /// <summary>
    /// Defines a contract for entities that expose domain events.
    /// </summary>
    public interface IHasDomainEvents
    {
        /// <summary>
        /// Gets the collection of domain events associated with the entity.
        /// </summary>
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Clears all domain events from the entity.
        /// </summary>
        void ClearDomainEvents();
    }
}
