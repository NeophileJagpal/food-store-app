using Ardalis.GuardClauses;
using FoodStore.SharedKernal.Exceptions;
using System.Text.RegularExpressions;

namespace FoodStore.SharedKernal
{
    public static class GaurdExtentions
    {
        private static Regex _emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled);
        public static void NullDefault<T>(this IGuardClause guardClause, T value, string parameterName)
        {
            guardClause.Null(value, parameterName);
            guardClause.Default(value, parameterName);
        }
        public static void InvalidEmail(this IGuardClause guardClause, string email, string parameterName)
        {
            if (_emailRegex.IsMatch(email) == false)
                throw new InvalidArgumentException($"Invalid {parameterName}");
        }
    }
}
