namespace RustRetail.SharedKernel.Domain.Events
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
