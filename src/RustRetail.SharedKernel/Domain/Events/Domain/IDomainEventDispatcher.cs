namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    /// <summary>
    /// Defines a contract for dispatching domain events.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches multiple domain events.
        /// </summary>
        /// <param name="domainEvents">The collection of domain events to dispatch.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task DispatchMultipleAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dispatches a single domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dispatches all domain events for the given entity.
        /// </summary>
        /// <param name="entity">The entity containing domain events.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task DispatchAsync(IHasDomainEvents entity, CancellationToken cancellationToken = default);
    }
}
