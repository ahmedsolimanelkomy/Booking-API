using AutoMapper;
using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.CarRental;
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
        private readonly IWebHostEnvironment _env;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Assets/Images/Car");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return $"/Assets/Images/Car/{uniqueFileName}";
        }

        public void DeletePhoto(string photoUrl)
        {
            var fileName = Path.GetFileName(photoUrl);
            var filePath = Path.Combine(_env.WebRootPath, "Assets/Images/Car", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<IEnumerable<FilteredCarDTO>> GetCarByBrand(string Brand)
        {
            var cars = await _unitOfWork.Cars.GetAllAsync(new[] { "CarPhotos" });

            var filteredCars = cars
                .Where(cars => cars.Brand != null && cars.Brand.Equals(Brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(filteredCars);
        }


        public async Task<IEnumerable<FilteredCarDTO>> GetFilteredCars(CarFilterationDTO filter)
        {
            var cars = await _unitOfWork.Cars.GetListAsync(car => true, new[] { "CarAgency", "CarAgency.City", "CarPhotos" });

            // Apply the filters
            var filteredCars = cars.Where(car =>
                (filter.CityId == 0 || (car.CarAgency?.CityId != null && car.CarAgency.CityId == filter.CityId)) &&
                (filter.PickUpDate == default(DateTime) || car.CarRentals.All(r => r.PickUpDate != filter.PickUpDate)) &&
                (filter.DropOffDate == default(DateTime) || car.CarRentals.All(r => r.DropOffDate != filter.DropOffDate)) &&
                (string.IsNullOrEmpty(filter.Description) || (car.Description != null && car.Description.Contains(filter.Description, StringComparison.OrdinalIgnoreCase))) &&
                (!filter.MinPrice.HasValue || (car.RentPrice.HasValue && car.RentPrice.Value >= filter.MinPrice.Value)) &&
                (!filter.MaxPrice.HasValue || (car.RentPrice.HasValue && car.RentPrice.Value <= filter.MaxPrice.Value)) &&
                (!filter.GearType.HasValue || car.GearType == filter.GearType) &&
                (!filter.ModelOfYear.HasValue || car.ModelOfYear == filter.ModelOfYear) &&
                (string.IsNullOrEmpty(filter.Brand) || (car.Brand != null && car.Brand.Equals(filter.Brand, StringComparison.OrdinalIgnoreCase))) &&
                (!filter.InsuranceIncluded.HasValue || car.InsuranceIncluded == filter.InsuranceIncluded) &&
                (!filter.NumberOfSeats.HasValue || car.NumberOfSeats == filter.NumberOfSeats) &&
                (!filter.AgencyId.HasValue || (car.CarAgency?.Id != null && car.CarAgency.Id == filter.AgencyId))
            ).ToList();

            // Map the filtered cars to FilteredCarDTO
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(filteredCars);
        }


        //get cars by agency id
        public async Task<IEnumerable<FilteredCarDTO>> GetCarsByAgencyId(int agencyId)
        {
            var cars = await _unitOfWork.Cars.GetListAsync(c => c.AgencyId == agencyId, new[] { "CarPhotos" });
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(cars);
        }

    }

}

