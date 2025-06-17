namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    /// <summary>
    /// Defines a contract for domain events within the domain model.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for the domain event.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
