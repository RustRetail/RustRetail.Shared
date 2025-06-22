using MediatR;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Represents a notification that wraps a domain event for use with MediatR.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// The type of the domain event being wrapped. Must implement <see cref="IDomainEvent"/>.
    /// </typeparam>
    public class DomainEventNotification<TDomainEvent>
        : INotification
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Gets the domain event associated with this notification.
        /// </summary>
        public TDomainEvent DomainEvent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventNotification{TDomainEvent}"/> class.
        /// </summary>
        /// <param name="domainEvent">The domain event to wrap in the notification.</param>
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
