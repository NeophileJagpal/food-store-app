using System.Collections.Generic;

namespace FoodStore.SharedKernal
{
    public interface IHasDomainEvent
    {
        public List<BaseDomainEvent> DomainEvents { get; set; }
    }
}
