using Booking_API.DTOs.CarAgencyDTOS;
using Booking_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services.IService
{
    public interface ICarAgencyService : IService<CarAgency>
    {
        //public Task<IEnumerable<CarAgencyViewDTO>> GetFilteredCarAgencies(CarAgencyFilterDTO carAgencyFilterDTO);
        Task<string> SavePhoto(IFormFile photo);
        void DeletePhoto(string photoUrl);

    }

}
