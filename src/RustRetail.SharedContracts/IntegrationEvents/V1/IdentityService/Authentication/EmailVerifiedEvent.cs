using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication
{
    public sealed class EmailVerifiedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public DateTimeOffset VerifiedAt { get; init; } = DateTimeOffset.UtcNow;

        public EmailVerifiedEvent(
            Guid userId,
            string email,
            DateTimeOffset verifiedAt)
        {
            UserId = userId;
            Email = email;
            VerifiedAt = verifiedAt;
        }
    }
}
