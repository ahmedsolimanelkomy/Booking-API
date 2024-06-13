using System.Linq.Expressions;

namespace Booking_API.Services.IService
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[]? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
