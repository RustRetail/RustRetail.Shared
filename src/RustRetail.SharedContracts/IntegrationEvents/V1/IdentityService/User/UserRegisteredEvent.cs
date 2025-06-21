using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.User
{
    public sealed class UserRegisteredEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTimeOffset RegisteredAt { get; init; } = DateTimeOffset.UtcNow;

        public UserRegisteredEvent(Guid userId, string userName, string email, DateTimeOffset registeredAt)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            RegisteredAt = registeredAt;
        }
    }
}
