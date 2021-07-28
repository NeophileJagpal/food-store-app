
using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using FoodStore.Domain.Entities.Aggregates.Product;
using FoodStore.Domain.ValueObjects;
using FoodStore.SharedKernal;
using FoodStore.SharedKernal.Interfaces;

namespace FoodStore.Domain.Entities
{
    public class Store : Entity<Guid>, IAggregateRoot
    {
        private Store()
        {
            this.Products = new List<Product>();
        }
        private Store(string name, Address address) : base(System.Guid.NewGuid())
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Null(address, nameof(address));

            this.Name = name;
            this.Address = address;
        }
        private Store(string name, Address address, ContactInfo contact) : this(name, address)
        {
            Guard.Against.Null(contact, nameof(contact));
            this.Contact = contact;
        }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ContactInfo Contact { get; set; }

        private List<Product> _products;
        public IEnumerable<Product> Products
        {
            get
            {
                return _products.AsReadOnly();
            }
            private set
            {
                _products = (List<Product>)value;
            }
        }

        public void AddProduct(Product product)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.DuplicateEntity<Product, Guid>(this._products, product, nameof(product));

            this._products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.DuplicateEntity<Product, Guid>(this._products, product, nameof(product));

            this._products.Add(product);
        }
    }
}