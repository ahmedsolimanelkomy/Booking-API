using Booking_API.DTOs.CarRental;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface ICarRentalService : IService<CarRental>
    {
        public Task<IEnumerable<CarRentalViewDTO>> GetFilteredCarRentals(CarRentalFilterationDTO filter);
        public Task<IEnumerable<CarRentalViewDTO>> GetFilteredUserCarRents(UserCarRentalFilterDTO filter);

    }
}
