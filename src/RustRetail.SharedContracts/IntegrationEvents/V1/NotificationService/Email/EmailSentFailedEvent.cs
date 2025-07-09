using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.NotificationService.Email
{
    public class EmailSentFailedEvent : IntegrationEvent
    {
        public Guid NotificationId { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;
        public DateTimeOffset LastAttemptAt { get; init; } = DateTimeOffset.UtcNow;
        public EmailSentFailedEvent(Guid notificationId, string errorMessage)
        {
            NotificationId = notificationId;
            ErrorMessage = errorMessage;
        }
    }
}
