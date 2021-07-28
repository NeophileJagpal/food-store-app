using System;

namespace FoodStore.SharedKernal.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        private DuplicateEntityException(string entityName,string entityIdentifier):base($"{entityName} with id:{entityIdentifier} already exist")
        {

        }
        public static DuplicateEntityException Create(string entityName, string entityIdentifier)
        {
            return new DuplicateEntityException(entityName, entityIdentifier);
        }
    }
}
