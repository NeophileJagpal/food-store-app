using Ardalis.GuardClauses;
using FoodStore.Domain.ValueObjects;
using FoodStore.SharedKernal;
using System;

namespace FoodStore.Domain.Entities.Aggregates.Product
{
    public class Customization : Entity<Guid>
    {
        private Customization()
        {

        }
        private Customization(Guid productId, string name, Money cost) : base(Guid.NewGuid())
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Null(cost, nameof(cost));
            Guard.Against.NullDefault(productId, nameof(productId));

            this.Name = name;
            this.Cost = cost;
            this.ProductId = productId;
        }
        public static Customization Create(Guid productId, string name, Money cost)
        {
            return new Customization(productId, name, cost);
        }

        public string Name { get; private set; }
        public Money Cost { get; private set; }
        public Guid ProductId { get; private set; }

    }
}