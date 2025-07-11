﻿namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    /// <summary>
    /// Represents a base class for domain events within the domain model.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <inheritdoc/>
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
