using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelPhotoService : IHotelPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<HotelPhoto> _repository;

        public HotelPhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<HotelPhoto>();
        }

        public async Task<IEnumerable<HotelPhoto>> GetAllAsync(string[]? includeProperties = null)
        {
            return await _repository.GetAllAsync(includeProperties);
        }

        public async Task<IEnumerable<HotelPhoto>> GetListAsync(Expression<Func<HotelPhoto, bool>> filter, string[]? includeProperties = null)
        {
            return await _repository.GetListAsync(filter, includeProperties);
        }

        public async Task<HotelPhoto> GetAsync(Expression<Func<HotelPhoto, bool>> filter, string[]? includeProperties = null)
        {
            return await _repository.GetAsync(filter, includeProperties);
        }
        public async Task<IEnumerable<HotelPhoto>> GetPhotosByHotelId(int hotelId)
                {
                    return await _repository.GetListAsync(p => p.HotelId == hotelId);
                }

        public async Task AddAsync(HotelPhoto entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(HotelPhoto entity)
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
