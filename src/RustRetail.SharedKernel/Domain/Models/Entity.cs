using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedKernel.Domain.Models
{
    /// <summary>
    /// Represents a base class for entities with a strongly-typed key and trackable timestamps.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    public abstract class Entity<TKey>
        : IHasKey<TKey>, ITrackable, IEquatable<Entity<TKey>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TKey}"/> class.
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TKey}"/> class with the specified key.
        /// Sets <see cref="CreatedDateTime"/> and <see cref="UpdatedDateTime"/> to the current UTC time.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        protected Entity(TKey id)
        {
            Id = id;
            CreatedDateTime = DateTimeOffset.UtcNow;
            UpdatedDateTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public TKey Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTimeOffset? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedDateTime { get; set; }

        /// <summary>
        /// Sets the creation date and time.
        /// </summary>
        /// <param name="createdDateTime">The creation timestamp to set.</param>
        public void SetCreatedDateTime(DateTimeOffset? createdDateTime)
        {
            CreatedDateTime = createdDateTime;
        }

        /// <summary>
        /// Sets the last updated date and time.
        /// </summary>
        /// <param name="updatedDateTime">The update timestamp to set.</param>
        public void SetUpdatedDateTime(DateTimeOffset? updatedDateTime)
        {
            UpdatedDateTime = updatedDateTime;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current entity.
        /// </summary>
        /// <param name="obj">The object to compare with the current entity.</param>
        /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            return obj is Entity<TKey> other && Equals(other.Id);
        }

        /// <summary>
        /// Determines whether two entities are equal.
        /// </summary>
        /// <param name="left">The first entity.</param>
        /// <param name="right">The second entity.</param>
        /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two entities are not equal.
        /// </summary>
        /// <param name="left">The first entity.</param>
        /// <param name="right">The second entity.</param>
        /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified entity is equal to the current entity.
        /// </summary>
        /// <param name="other">The entity to compare with the current entity.</param>
        /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Entity<TKey>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// Returns a hash code for the entity based on its key.
        /// </summary>
        /// <returns>A hash code for the current entity.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<TKey>.Default.GetHashCode(Id!);
        }
    }
}
