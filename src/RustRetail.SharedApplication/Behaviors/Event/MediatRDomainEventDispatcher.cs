using MediatR;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedApplication.Behaviors.Event
{
    public class MediatRDomainEventDispatcher(IPublisher publisher)
        : IDomainEventDispatcher
    {
        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent);
            await publisher.Publish((INotification)notification!, cancellationToken);
        }

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
