using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.User
{
    public sealed class UserUpdatedProfileEvent : IntegrationEvent
    {
        public UserUpdatedProfileEvent(Guid userId, DateTimeOffset updatedAt)
        {
            UserId = userId;
            UpdatedAt = updatedAt;
        }

        public Guid UserId { get; init; }
        public DateTimeOffset UpdatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}
