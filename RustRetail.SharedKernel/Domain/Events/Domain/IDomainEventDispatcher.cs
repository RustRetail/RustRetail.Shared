namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    public interface IDomainEventDispatcher
    {
        Task DispatchMultipleAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
