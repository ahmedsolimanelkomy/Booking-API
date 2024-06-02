using Booking_API.Models;
using Booking_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class BookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(string[]? includeProperties = null)
        {
            return await _unitOfWork.Bookings.GetAllAsync(includeProperties);
        }

        public async Task<Booking> GetAsync(Expression<Func<Booking, bool>> filter, string[]? includeProperties = null)
        {
            return await _unitOfWork.Bookings.GetAsync(filter,includeProperties);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _unitOfWork.Bookings.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }

}
