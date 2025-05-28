using MediatR;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedApplication.Abstractions
{
    public class DomainEventNotification<TDomainEvent>
        : INotification
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
