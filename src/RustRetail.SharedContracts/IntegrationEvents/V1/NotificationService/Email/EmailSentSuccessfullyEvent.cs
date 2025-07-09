using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.NotificationService.Email
{
    public class EmailSentSuccessfullyEvent : IntegrationEvent
    {
        public Guid NotificationId { get; init; }
        public DateTimeOffset SentAt { get; init; } = DateTimeOffset.UtcNow;
        public EmailSentSuccessfullyEvent(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
