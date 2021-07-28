using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using FoodStore.Domain.ValueObjects;
using FoodStore.SharedKernal;
using FoodStore.SharedKernal.Interfaces;

namespace FoodStore.Domain.Entities.Aggregates.Product
{
    public class Product : Entity<Guid>, IAggregateRoot
    {
        private Product()
        {

        }
        private Product(Guid storeId, ProductInfo productInfo, Money cost) : base(Guid.NewGuid())
        {
            Guard.Against.NullDefault(storeId, nameof(storeId));
            Guard.Against.Null(productInfo, nameof(productInfo));
            Guard.Against.Null<Money>(cost, nameof(cost));

            this.ProductInfo = productInfo;
            this.Cost = cost;
            this.StoreId = storeId;
            this.Customizations = new List<Customization>();
        }

        public ProductInfo ProductInfo { get; private set; }
        public Money Cost { get; private set; }

        private List<Customization> _customizations;
        public IEnumerable<Customization> Customizations
        {
            get
            {
                return _customizations.AsReadOnly(); //AsReadOnly can't be cast to a list!
            }
            private set
            {
                _customizations = (List<Customization>)value;
            }
        }
        public Guid StoreId { get; private set; }

        public static Product Create(Guid storeId, ProductInfo productInfo, Money cost)
        {
            return new Product(storeId, productInfo, cost);
        }

        public void UpdateInfo(ProductInfo productInfo)
        {
            Guard.Against.Null(productInfo, nameof(productInfo));
            this.ProductInfo = productInfo;
        }

        public void AddCustomization(Customization customization)
        {
            Guard.Against.Null(customization, nameof(customization));
            Guard.Against.DuplicateEntity<Customization, Guid>(this._customizations, customization, nameof(customization));

            this._customizations.Add(customization);
        }

        public void RemoveCustomization(Customization customization)
        {
            Guard.Against.Null(customization, nameof(customization));
            Guard.Against.EntityNotExist<Customization, Guid>(this._customizations, customization, nameof(customization));

            this._customizations.Remove(customization);
        }
    }
}