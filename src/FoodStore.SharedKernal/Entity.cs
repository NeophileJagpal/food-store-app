using Ardalis.Specification;
using System;

namespace FoodStore.SharedKernal
{
    public abstract class Entity<TId> : IEntity<TId> where TId : IEquatable<TId>
    {
        protected Entity()
        {

        }
        protected Entity(TId id)
        {
            this.Id = id;
        }
        public TId Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;
            if (ReferenceEquals(other, null) || GetType() != other.GetType() || this.Id.Equals(default(TId)))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}