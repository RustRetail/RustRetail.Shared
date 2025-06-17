namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for entities that support optimistic concurrency using a row version token.
    /// </summary>
    public interface IHasConcurrencyToken
    {
        /// <summary>
        /// Gets or sets the row version used for concurrency checks.
        /// </summary>
        byte[] RowVersion { get; set; }
    }
}
