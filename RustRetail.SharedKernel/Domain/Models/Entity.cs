using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedKernel.Domain.Models
{
    public abstract class Entity<TKey>
        : IHasKey<TKey>, ITrackable, IEquatable<Entity<TKey>>
    {
        protected Entity() { }

        protected Entity(TKey id)
        {
            Id = id;
            CreatedDateTime = DateTimeOffset.UtcNow;
            UpdatedDateTime = DateTimeOffset.UtcNow;
        }

        public TKey Id { get; set; } = default!;
        public DateTimeOffset? CreatedDateTime { get; private set; }
        public DateTimeOffset? UpdatedDateTime { get; private set; }

        public void SetCreatedDateTime(DateTimeOffset? createdDateTime)
        {
            CreatedDateTime = createdDateTime;
        }

        public void SetUpdatedDateTime(DateTimeOffset? updatedDateTime)
        {
            UpdatedDateTime = updatedDateTime;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            return obj is Entity<TKey> other && Equals(other.Id);
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Entity<TKey>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TKey>.Default.GetHashCode(Id!);
        }
    }
}
