using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.SharedPersistence.Interceptors
{
    /// <summary>
    /// Intercepts EF Core save changes operations to handle and dispatch domain events before changes are persisted.
    /// </summary>
    /// <remarks>
    /// This interceptor scans tracked entities for domain events, dispatches them, and clears the events before saving changes.
    /// </remarks>
    public class DomainEventHandlingInterceptor(IDomainEventDispatcher dispatcher)
        : SaveChangesInterceptor
    {
        /// <summary>
        /// Called asynchronously before EF Core saves changes to the database.
        /// Handles and dispatches domain events for tracked entities.
        /// </summary>
        /// <param name="eventData">The event data associated with the save operation.</param>
        /// <param name="result">The interception result.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing the interception result.
        /// </returns>
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await HandleDomainEvents(eventData.Context, cancellationToken);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Scans the given <see cref="DbContext"/> for entities with domain events, dispatches those events, and clears them.
        /// </summary>
        /// <param name="context">The database context to scan for domain events.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        private async Task HandleDomainEvents(DbContext? context, CancellationToken cancellationToken = default)
        {
            if (context is null) return;

            var domainEntities = context.ChangeTracker
                .Entries<IHasDomainEvents>()
                .Where(entry => entry.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(entry => entry.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(e => e.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await dispatcher.DispatchAsync(domainEvent, cancellationToken);
            }
        }
    }
}
