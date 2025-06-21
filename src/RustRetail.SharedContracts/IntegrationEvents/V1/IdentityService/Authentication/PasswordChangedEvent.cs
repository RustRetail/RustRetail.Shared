using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication
{
    public sealed class PasswordChangedEvent : IntegrationEvent
    {
        public PasswordChangedEvent(Guid userId, DateTimeOffset changedAt)
        {
            UserId = userId;
            ChangedAt = changedAt;
        }

        public Guid UserId { get; init; }
        public DateTimeOffset ChangedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}
