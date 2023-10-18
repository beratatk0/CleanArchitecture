using System.Linq.Expressions;

namespace Clean.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> AddAsync(T item);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task RemoveAsync(T item);

        Task RemoveRangeAsync(IEnumerable<T> items);

        Task UpdateAsync(T item);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}