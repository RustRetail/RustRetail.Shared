using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication
{
    public sealed class EmailVerificationRequestedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string VerificationToken { get; init; } = string.Empty;
        public DateTimeOffset RequestedAt { get; init; } = DateTimeOffset.UtcNow;

        public EmailVerificationRequestedEvent(Guid userId,
            string email,
            string verificationToken,
            DateTimeOffset requestedAt)
        {
            UserId = userId;
            Email = email;
            VerificationToken = verificationToken;
            RequestedAt = requestedAt;
        }
    }
}
