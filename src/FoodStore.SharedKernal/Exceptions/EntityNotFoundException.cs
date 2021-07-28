using System;

namespace FoodStore.SharedKernal.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private EntityNotFoundException(string entityName, string entityIdentifier) : base($"{entityName} with id:{entityIdentifier} not found")
        {

        }
        public static EntityNotFoundException Create(string entityName, string entityIdentifier)
        {
            return new EntityNotFoundException(entityName, entityIdentifier);
        }
    }
}
