namespace RustRetail.SharedContracts.IntegrationEvents.Abstractions
{
    /// <summary>
    /// Defines a contract for integration events used for cross-service communication.
    /// </summary>
    public interface IIntegrationEvent
    {
        /// <summary>
        /// Gets the unique identifier for the integration event.
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
