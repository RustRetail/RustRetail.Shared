using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedKernel.Domain.Models
{
    /// <summary>
    /// Represents the root entity of an aggregate, supporting domain events.
    /// </summary>
    /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
    public abstract class AggregateRoot<TKey>
        : Entity<TKey>, IHasDomainEvents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TKey}"/> class.
        /// </summary>
        protected AggregateRoot() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TKey}"/> class with the specified key.
        /// </summary>
        /// <param name="id">The unique identifier for the aggregate root.</param>
        protected AggregateRoot(TKey id) : base(id) { }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Gets the domain events associated with this aggregate root.
        /// </summary>
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all domain events from the aggregate root.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();

        /// <summary>
        /// Adds a domain event to the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="domainEvent"/> is null.</exception>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
                throw new ArgumentNullException(nameof(domainEvent));
            _domainEvents.Add(domainEvent);
        }
    }
}
