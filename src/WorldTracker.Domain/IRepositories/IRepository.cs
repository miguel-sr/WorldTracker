using WorldTracker.Common.Interfaces;

namespace WorldTracker.Domain.IRepositories
{
    public interface IRepository<T, TId> where T : class, IEntity<TId>
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(TId id);
        Task Update(T entity);
        Task Delete(TId id);
    }
}
