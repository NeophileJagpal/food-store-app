using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodStore.SharedKernal.Interfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task SaveChangesAsync();
    }
}
