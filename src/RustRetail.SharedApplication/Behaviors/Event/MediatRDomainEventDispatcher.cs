using MediatR;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedApplication.Behaviors.Event
{
    /// <summary>
    /// Dispatches domain events using MediatR for notification publishing.
    /// </summary>
    public class MediatRDomainEventDispatcher(IPublisher publisher)
        : IDomainEventDispatcher
    {
        /// <inheritdoc/>
        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent);
            await publisher.Publish((INotification)notification!, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DispatchMultipleAsync(IEnumerable<IDomainEvent> domainEvents,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var notification = Activator.CreateInstance(notificationType, domainEvent);
                await publisher.Publish((INotification)notification!, cancellationToken);
            }
        }
    }
}
