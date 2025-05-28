using MediatR;

namespace RustRetail.SharedApplication.Abstractions
{
    public interface IDomainEventHandler<TEvent>
        : INotificationHandler<TEvent>
        where TEvent : INotification
    {
    }
}
