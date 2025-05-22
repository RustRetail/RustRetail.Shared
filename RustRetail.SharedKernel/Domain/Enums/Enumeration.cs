using System.Reflection;

namespace RustRetail.SharedKernel.Domain.Enums
{
    /// <summary>
    /// A base class for strongly-typed, rich enumeration objects with name and value.
    /// Use this pattern instead of C# enums when you need more power (e.g., methods, validation).
    /// Inspired by Jimmy Bogard's Enumeration pattern.
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Gets the name of the enumeration instance.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the numeric value of the enumeration instance.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="name">The display name.</param>
        /// <param name="value">The numeric value.</param>
        protected Enumeration(string name, int value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Returns the name of the enumeration instance.
        /// </summary>
        public override string ToString() => Name;

        /// <summary>
        /// Checks equality between this instance and another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the types and values are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue) return false;
            return GetType() == obj.GetType() && Value == otherValue.Value;
        }

        /// <summary>
        /// Gets the hash code for this enumeration instance.
        /// </summary>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Compares this instance to another object based on Value.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>A signed integer that indicates the relative position in the sort order.</returns>
        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            return Value.CompareTo(((Enumeration)obj).Value);
        }

        /// <summary>
        /// Returns all instances of the given enumeration type.
        /// </summary>
        /// <typeparam name="T">The type of enumeration to retrieve.</typeparam>
        /// <returns>An enumerable of all defined instances of <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        /// <summary>
        /// Returns the absolute difference between two enumeration instances.
        /// </summary>
        /// <param name="firstValue">The first enumeration instance.</param>
        /// <param name="secondValue">The second enumeration instance.</param>
        /// <returns>The absolute integer difference between their values.</returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            if (firstValue is null) throw new ArgumentNullException(nameof(firstValue));
            if (secondValue is null) throw new ArgumentNullException(nameof(secondValue));
            return Math.Abs(firstValue.Value - secondValue.Value);
        }

        /// <summary>
        /// Retrieves an enumeration instance by its numeric value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The numeric value.</param>
        /// <returns>The matching enumeration instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no match is found.</exception>
        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(item => item.Value == value);
            if (matchingItem is null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid value for {typeof(T).Name}");
            }
            return matchingItem;
        }

        /// <summary>
        /// Retrieves an enumeration instance by its name (case-insensitive).
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="name">The name of the enumeration.</param>
        /// <returns>The matching enumeration instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no match is found.</exception>
        public static T FromName<T>(string name) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(item => item.Name == name);
            if (matchingItem is null)
            {
                throw new InvalidOperationException($"'{name}' is not a valid name for {typeof(T).Name}");
            }
            return matchingItem;
        }
    }
}
