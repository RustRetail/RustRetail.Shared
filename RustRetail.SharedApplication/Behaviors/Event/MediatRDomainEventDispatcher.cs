using MediatR;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedApplication.Behaviors.Event
{
    public class MediatRDomainEventDispatcher(IMediator mediator)
        : IDomainEventDispatcher
    {
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var notification = Activator.CreateInstance(notificationType, domainEvent);
                await mediator.Publish((INotification)notification!, cancellationToken);
            }
        }
    }
}
