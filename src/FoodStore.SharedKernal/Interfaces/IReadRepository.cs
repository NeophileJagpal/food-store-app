using Ardalis.Specification;

namespace FoodStore.SharedKernal.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }   
}
