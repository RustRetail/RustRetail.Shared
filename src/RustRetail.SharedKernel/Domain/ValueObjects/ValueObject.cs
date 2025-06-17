namespace RustRetail.SharedKernel.Domain.ValueObjects
{
    /// <summary>
    /// Represents a base class for value objects, providing equality logic based on component values.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Gets the atomic values that define equality for the value object.
        /// </summary>
        /// <returns>An enumerable of the components used for equality comparison.</returns>
        public abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current value object.
        /// </summary>
        /// <param name="obj">The object to compare with the current value object.</param>
        /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }
            var valueObject = (ValueObject)obj;
            return GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());
        }

        /// <summary>
        /// Determines whether two value objects are equal.
        /// </summary>
        /// <param name="left">The first value object.</param>
        /// <param name="right">The second value object.</param>
        /// <returns><c>true</c> if the value objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two value objects are not equal.
        /// </summary>
        /// <param name="left">The first value object.</param>
        /// <param name="right">The second value object.</param>
        /// <returns><c>true</c> if the value objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a hash code for the value object based on its equality components.
        /// </summary>
        /// <returns>A hash code for the current value object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Determines whether the specified value object is equal to the current value object.
        /// </summary>
        /// <param name="other">The value object to compare with the current value object.</param>
        /// <returns><c>true</c> if the value objects are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}
