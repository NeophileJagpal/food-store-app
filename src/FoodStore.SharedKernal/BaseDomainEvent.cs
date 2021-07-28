using System;

namespace FoodStore.SharedKernal
{
    public abstract class BaseDomainEvent
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
        public bool IsPublished { get; set; }
    }
}
