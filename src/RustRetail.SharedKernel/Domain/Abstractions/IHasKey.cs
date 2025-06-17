namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for entities that have a strongly-typed key.
    /// </summary>
    /// <typeparam name="T">The type of the key.</typeparam>
    public interface IHasKey<T>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        T Id { get; set; }
    }
}
