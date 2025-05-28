using RustRetail.SharedKernel.Domain.Events;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedKernel.Domain.Models
{
    public abstract class AggregateRoot<TKey>
        : Entity<TKey>, IHasDomainEvents
    {
        protected AggregateRoot() : base() { }
        protected AggregateRoot(TKey id) : base(id) { }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
                throw new ArgumentNullException(nameof(domainEvent));
            _domainEvents.Add(domainEvent);
        }
    }
}
