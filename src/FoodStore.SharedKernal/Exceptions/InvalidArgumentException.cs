using System;

namespace FoodStore.SharedKernal.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string message):base(message)
        {

        }
    }
}