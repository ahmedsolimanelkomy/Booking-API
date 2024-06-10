using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[]? includeProperties = null)
        {
            return await _repository.GetAllAsync(includeProperties);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter,string[]? includeProperties = null)
        {
            return await _repository.GetListAsync(filter, includeProperties);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null)
        {
            return await _repository.GetAsync(filter, includeProperties);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
