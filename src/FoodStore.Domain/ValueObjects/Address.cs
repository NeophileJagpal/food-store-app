using System.Collections.Generic;
using Ardalis.GuardClauses;
using FoodStore.SharedKernal;

namespace FoodStore.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private Address(string addressLine, string city, string country, string zipCode)
        {
            Guard.Against.NullOrWhiteSpace(addressLine, nameof(addressLine));
            Guard.Against.NullOrWhiteSpace(city, nameof(city));
            Guard.Against.NullOrWhiteSpace(country, nameof(country));
            Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));

            this.AddressLine = addressLine;
            this.City = city;
            this.Country = country;
            this.ZipCode = zipCode;
        }
        public string AddressLine { get; }
        public string City { get; }
        public string Country { get; }
        public string ZipCode { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressLine;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }

        public static Address Create(string addressLine, string city, string country, string zipCode)
        {
            return new Address(addressLine, city, country, zipCode);
        }
    }
}