using Ardalis.GuardClauses;
using FoodStore.SharedKernal;
using System.Collections.Generic;

namespace FoodStore.Domain.ValueObjects
{
    public class ContactInfo : ValueObject
    {
        private ContactInfo(string email, string phone)
        {
            Guard.Against.InvalidEmail(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(phone, nameof(phone));

            this.Email = email;
            this.Phone = phone;
        }
        public static ContactInfo Create(string email, string phone)
        {
            return new ContactInfo(email, phone);
        }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
            yield return Phone;
        }
    }
}
