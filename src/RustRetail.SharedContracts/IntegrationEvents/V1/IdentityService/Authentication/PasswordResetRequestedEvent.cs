using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication
{
    public sealed class PasswordResetRequestedEvent : IntegrationEvent
    {
        public PasswordResetRequestedEvent(
            Guid userId,
            string email,
            string resetToken,
            DateTimeOffset requestedAt)
        {
            UserId = userId;
            Email = email;
            ResetToken = resetToken;
            RequestedAt = requestedAt;
        }

        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string ResetToken { get; init; } = string.Empty;
        public DateTimeOffset RequestedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}
