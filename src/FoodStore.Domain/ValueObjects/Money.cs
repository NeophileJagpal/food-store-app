using System.Collections.Generic;
using Ardalis.GuardClauses;
using FoodStore.SharedKernal;

namespace FoodStore.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        private Money(string currency, decimal amount)
        {
            Guard.Against.NullOrWhiteSpace(currency, nameof(currency));
            Guard.Against.Negative(amount, nameof(amount), $"{nameof(amount)} should be negative");

            this.Currency = currency;
            this.Amount = amount;
        }
        public string Currency { get; }
        public decimal Amount { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Amount;
        }
        public static Money Create(string currency, decimal amount)
        {
            return new Money(currency, amount);
        }
    }
}