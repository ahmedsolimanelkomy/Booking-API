using Booking_API.DTOs.CarAgencyDTOS;
using Booking_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services.IService
{
    public interface ICarAgencyService : IService<CarAgency>
    {
        public Task<IEnumerable<CarAgencyDTO>> GetFilteredCarAgencies(CarAgencyFilterDTO carAgencyFilterDTO);
    }

}
