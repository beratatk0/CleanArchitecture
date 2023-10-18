using System.Linq.Expressions;

namespace Clean.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T item);

        Task AddRangeAsync(IEnumerable<T> items);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        void Update(T item);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}