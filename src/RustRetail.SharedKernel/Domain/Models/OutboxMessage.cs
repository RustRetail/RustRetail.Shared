namespace RustRetail.SharedKernel.Domain.Models
{
    /// <summary>
    /// Represents a message stored in the outbox for reliable event publishing.
    /// </summary>
    public sealed class OutboxMessage
    {
        /// <summary>
        /// Gets or sets the unique identifier for the outbox message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the serialized content of the message.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the message occurred.
        /// </summary>
        public DateTimeOffset OccurredOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the message was processed, if applicable.
        /// </summary>
        public DateTimeOffset? ProcessedOn { get; set; }

        /// <summary>
        /// Gets or sets the error message if processing failed.
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage"/> class with the specified type and content.
        /// </summary>
        /// <param name="type">The type of the message.</param>
        /// <param name="content">The serialized content of the message.</param>
        public OutboxMessage(string type, string content)
        {
            Type = type;
            Content = content;

            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.UtcNow;
            ProcessedOn = null;
            Error = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage"/> class.
        /// </summary>
        public OutboxMessage()
        {
        }
    }
}
