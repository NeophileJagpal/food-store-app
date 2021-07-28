using Ardalis.GuardClauses;
using FoodStore.SharedKernal;
using System.Collections.Generic;

namespace FoodStore.Domain.ValueObjects
{
    public class ProductInfo : ValueObject
    {
        private ProductInfo(string name, string description)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            this.Name = name;
            this.Description = description;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }

        public static ProductInfo Create(string name, string description)
        {
            return new ProductInfo(name, description);
        }
    }
}
