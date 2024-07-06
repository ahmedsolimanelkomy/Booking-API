using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services.IService
{
    public interface ICarService : IService<Car>
    {
        public Task<IEnumerable<FilteredCarDTO>> GetCarByBrand(string Brand);
        public Task<IEnumerable<FilteredCarDTO>> GetFilteredCars(CarFilterationDTO filter);

    }
}
