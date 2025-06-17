namespace RustRetail.SharedKernel.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with an amount and currency.
    /// </summary>
    public class Money : ValueObject
    {
        /// <summary>
        /// Gets the monetary amount.
        /// </summary>
        public decimal Amount { get; }
        /// <summary>
        /// Gets the currency code.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The monetary amount.</param>
        /// <param name="currency">The currency code.</param>
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Gets the components used for equality comparison.
        /// </summary>
        /// <returns>An enumerable containing the amount and currency.</returns>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
