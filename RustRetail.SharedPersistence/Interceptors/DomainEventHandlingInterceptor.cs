using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using RustRetail.SharedKernel.Domain.Events.Domain;
using RustRetail.SharedKernel.Domain.Events.Integration;
using RustRetail.SharedKernel.Domain.Models;
using RustRetail.SharedPersistence.Abstraction;

namespace RustRetail.SharedPersistence.Interceptors
{
    public class DomainEventHandlingInterceptor(IDomainEventDispatcher dispatcher)
        : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await HandleDomainEvents(eventData.Context, cancellationToken);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

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

                // Handle integration event
                // Convert to integration event and check if DbContext supports outbox pattern
                if (domainEvent is IIntegrationEvent integrationEvent && context is IHasOutboxMessage)
                {
                    await context.Set<OutboxMessage>().AddAsync(
                        new OutboxMessage(
                            type: integrationEvent.GetType().Name,
                            content: JsonConvert.SerializeObject(
                                value: integrationEvent,
                                settings: new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                })),
                        cancellationToken);
                }
            }
        }
    }
}
