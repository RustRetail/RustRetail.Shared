namespace RustRetail.SharedApplication.Behaviors.Messaging
{
    /// <summary>
    /// Defines a contract for publishing messages to a message bus.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publishes a message of the specified type to the message bus.
        /// </summary>
        /// <typeparam name="T">The type of the message to publish.</typeparam>
        /// <param name="message">The message instance to publish.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous publish operation.</returns>
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class;
        /// <summary>
        /// Publishes a message to the message bus using the specified type.
        /// </summary>
        /// <param name="message">The message instance to publish.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous publish operation.</returns>
        Task PublishAsync(object message, Type type, CancellationToken cancellationToken = default);
    }
}
