using AutoMapper;
using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class CarService : Service<Car>, ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilteredCarDTO>> GetCarByBrand(string Brand)
        {
            var cars = await _unitOfWork.Cars.GetAllAsync();

            var filteredCars = cars
                .Where(cars => cars.Brand != null && cars.Brand.Equals(Brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(filteredCars);
        }
    }

}

