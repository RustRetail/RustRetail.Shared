using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedKernel.Domain.Events.Integration
{
    /// <summary>
    /// Represents a base class for integration events.
    /// </summary>
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
