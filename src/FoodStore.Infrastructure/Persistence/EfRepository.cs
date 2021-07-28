using Ardalis.Specification;
using FoodStore.SharedKernal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Persistence
{
    public class EfRepository<T> : EfReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
        {

        }
        public virtual async Task<T> AddAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);

            await SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);

            await SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);

            await SaveChangesAsync();
        }

        public virtual async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
