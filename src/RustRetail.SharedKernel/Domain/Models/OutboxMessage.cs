namespace RustRetail.SharedKernel.Domain.Models
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset OccurredOn { get; set; }
        public DateTimeOffset? ProcessedOn { get; set; }
        public string? Error { get; set; }

        public OutboxMessage(string type, string content)
        {
            Type = type;
            Content = content;

            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.UtcNow;
            ProcessedOn = null;
            Error = null;
        }

        public OutboxMessage()
        {
        }
    }
}
