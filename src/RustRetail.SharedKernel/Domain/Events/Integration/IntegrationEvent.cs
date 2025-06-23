namespace RustRetail.SharedKernel.Domain.Events.Integration
{
    /// <summary>
    /// Represents a base class for integration events.
    /// </summary>
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        /// <inheritdoc/>
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
