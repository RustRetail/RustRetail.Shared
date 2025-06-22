using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedApplication.Behaviors.Outbox
{
    /// <summary>
    /// Defines a contract for managing outbox messages to support reliable event publishing.
    /// </summary>
    public interface IOutboxMessageService
    {
        /// <summary>
        /// Adds a single outbox message to the store asynchronously.
        /// </summary>
        /// <param name="message">The outbox message to add.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddOutboxMessageAsync(OutboxMessage message, CancellationToken cancellationToken = default);
        /// <summary>
        /// Adds multiple outbox messages to the store asynchronously.
        /// </summary>
        /// <param name="messages">The collection of outbox messages to add.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddOutboxMessagesAsync(IEnumerable<OutboxMessage> messages, CancellationToken cancellationToken = default);
        /// <summary>
        /// Retrieves a list of unpublished outbox messages.
        /// </summary>
        /// <param name="takeSize">The maximum number of messages to retrieve. Default is 20.</param>
        /// <param name="asTracking">Whether to track the entities in the context. Default is true.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of unpublished messages.</returns>
        Task<List<OutboxMessage>> GetUnpublishedMessagesAsync(int takeSize = 20, bool asTracking = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates a single outbox message in the store.
        /// </summary>
        /// <param name="message">The outbox message to update.</param>
        void UpdateOutboxMessage(OutboxMessage message);
        /// <summary>
        /// Updates multiple outbox messages in the store.
        /// </summary>
        /// <param name="messages">The collection of outbox messages to update.</param>
        void UpdateOutboxMessages(IEnumerable<OutboxMessage> messages);
        /// <summary>
        /// Persists all changes made in the outbox message store asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Retrieves a list of outbox messages that encountered errors during processing.
        /// </summary>
        /// <param name="takeSize">The maximum number of error messages to retrieve. Default is 20.</param>
        /// <param name="asTracking">Whether to track the entities in the context. Default is true.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of error messages.</returns>
        Task<List<OutboxMessage>> GetErrorMessagesAsync(int takeSize = 20, bool asTracking = true, CancellationToken cancellationToken = default);
    }
}
