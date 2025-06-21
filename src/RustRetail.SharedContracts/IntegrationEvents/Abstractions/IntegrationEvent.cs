namespace RustRetail.SharedContracts.IntegrationEvents.Abstractions
{
    /// <summary>
    /// Represents a base class for integration events.
    /// </summary>
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }
}
