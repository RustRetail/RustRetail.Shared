using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedPersistence.Abstraction
{
    /// <summary>
    /// Defines a contract for entities that expose an <see cref="OutboxMessage"/> set for reliable event publishing.
    /// </summary>
    public interface IHasOutboxMessage
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of outbox messages used for the outbox pattern.
        /// </summary>
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
    }
}
