namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
