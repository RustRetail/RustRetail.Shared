using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication
{
    public sealed class UserLockedOutEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Reason { get; init; } = string.Empty;
        public int LockoutDurationInMilliseconds { get; init; } = 0;

        public UserLockedOutEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
