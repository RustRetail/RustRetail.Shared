namespace RustRetail.SharedKernel.Domain.Events.Integration
{
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
