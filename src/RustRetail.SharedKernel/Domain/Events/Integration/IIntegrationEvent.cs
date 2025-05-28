namespace RustRetail.SharedKernel.Domain.Events.Integration
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
