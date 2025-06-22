using MediatR;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Defines a handler for processing domain events of a specified type.
    /// </summary>
    /// <typeparam name="TEvent">The type of the domain event to handle. Must implement <see cref="INotification"/>.</typeparam>
    public interface IDomainEventHandler<TEvent>
        : INotificationHandler<TEvent>
        where TEvent : INotification
    {
    }
}
