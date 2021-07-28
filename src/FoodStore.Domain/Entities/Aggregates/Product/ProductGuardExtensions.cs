using Ardalis.GuardClauses;
using FoodStore.SharedKernal;
using FoodStore.SharedKernal.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodStore.Domain.Entities.Aggregates.Product
{
    public static class ProductGuardExtensions
    {
        public static void DuplicateEntity<T, TId>(this IGuardClause guardClause, IEnumerable<T> existingEntities, T newEntity, string parameterName)
            where T : Entity<TId>
            where TId : IEquatable<TId>
        {
            if (existingEntities.Any(a => a.Id.Equals(newEntity.Id)))
            {
                throw DuplicateEntityException.Create(parameterName, newEntity.Id.ToString());
            }
        }
        public static void EntityNotExist<T, TId>(this IGuardClause guardClause, IEnumerable<T> existingEntities, T newEntity, string parameterName)
            where T : Entity<TId>
            where TId : IEquatable<TId>
        {
            if (existingEntities.Any(a => a.Id.Equals(newEntity.Id)) == false)
            {
                throw EntityNotFoundException.Create(parameterName, newEntity.Id.ToString());
            }
        }
    }
}
