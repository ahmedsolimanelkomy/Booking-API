using AutoMapper;
using Booking_API.DTOs.CarAgencyDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services
{
    public class CarAgencyServices : Service<CarAgency>, ICarAgencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarAgencyServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarAgencyDTO>> GetFilteredCarAgencies(CarAgencyFilterDTO carAgencyFilterDTO)
        {
            IEnumerable<CarAgency> CarAgencies = await _unitOfWork.CarAgencies.GetAllAsync(new[] {"City"} );
            var FilteredAgencies = CarAgencies.Where(CarAgency => 
            (!carAgencyFilterDTO.CityId.HasValue || CarAgency.CityId == carAgencyFilterDTO.CityId) &&
            (string.IsNullOrEmpty(carAgencyFilterDTO.Name) || CarAgency.Name.Contains(carAgencyFilterDTO.Name, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(carAgencyFilterDTO.Address) || CarAgency.Address.Contains(carAgencyFilterDTO.Address, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            return _mapper.Map<IEnumerable<CarAgencyDTO>>(FilteredAgencies);
        }
    }
}
