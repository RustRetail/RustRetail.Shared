using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.NotificationService.Email
{
    public class EmailSendRequestedEvent : IntegrationEvent
    {
        public Guid NotificationId { get; init; }
        public string To { get; init; } = string.Empty;
        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;

        public EmailSendRequestedEvent(Guid notificationId, string to, string subject, string body)
        {
            NotificationId = notificationId;
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
