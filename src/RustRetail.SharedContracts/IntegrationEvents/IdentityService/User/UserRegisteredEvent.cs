using RustRetail.SharedKernel.Domain.Events.Integration;

namespace RustRetail.SharedContracts.IntegrationEvents.IdentityService.User
{
    public sealed class UserRegisteredEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserRegisteredEvent(Guid userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}
