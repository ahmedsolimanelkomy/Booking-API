using AutoMapper;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;
using Booking_API.Repository;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Booking_API.Services
{
    public class CarRentalService : Service<CarRental>, ICarRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarRentalService(IUnitOfWork unitOfWork, IMapper mapper ) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public CarRentalService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<CarRentalViewDTO>> GetFilteredCarRentals(CarRentalFilterationDTO filter)
        {
            var rentals = await _unitOfWork.CarRental.GetAllAsync(new[] { "Car", "CarAgency", "Car.CarPhotos", "ApplicationUser" });

            var filteredRentals = rentals
                .Where(rental =>
                    (!filter.AgencyId.HasValue || rental.CarAgency.Id == filter.AgencyId.Value) &&
                    rental.PickUpDate.Date == filter.PickUpDate &&
                    rental.DropOffDate.Date == filter.DropOffDate &&
                    (!filter.GearType.HasValue || rental.Car.GearType == filter.GearType) &&
                    (!filter.ModelOfYear.HasValue || rental.Car.ModelOfYear == filter.ModelOfYear) &&
                    (string.IsNullOrEmpty(filter.Brand) || rental.Car.Brand.Contains(filter.Brand, StringComparison.OrdinalIgnoreCase)) &&
                    (!filter.NumberOfSeats.HasValue || rental.Car.NumberOfSeats == filter.NumberOfSeats) &&
                    (!filter.InsuranceIncluded.HasValue || rental.Car.InsuranceIncluded == filter.InsuranceIncluded) &&
                    (!filter.CityId.HasValue || rental.CarAgency.CityId == filter.CityId.Value)
                ).ToList();

            return _mapper.Map<IEnumerable<CarRentalViewDTO>>(filteredRentals);
        }

        public async Task<IEnumerable<CarRentalViewDTO>> GetFilteredUserCarRents(UserCarRentalFilterDTO filter)
        {
            var rentals = await _unitOfWork.CarRental.GetAllAsync(new[] { "Car", "CarAgency" });

            var filteredRentals = rentals
                .Where(rental =>
                    (string.IsNullOrEmpty(filter.AgencyName) || rental.CarAgency.Name.Contains(filter.AgencyName, StringComparison.OrdinalIgnoreCase)) &&
                    (!filter.PickUpDate.HasValue || rental.PickUpDate.Date == filter.PickUpDate.Value.Date) &&
                    (!filter.DropOffDate.HasValue || rental.DropOffDate.Date == filter.DropOffDate.Value.Date) &&
                    (!filter.GearType.HasValue || rental.Car.GearType == filter.GearType) &&
                    (!filter.ModelOfYear.HasValue || rental.Car.ModelOfYear == filter.ModelOfYear) &&
                    (string.IsNullOrEmpty(filter.Brand) || rental.Car.Brand.Contains(filter.Brand, StringComparison.OrdinalIgnoreCase)) &&
                    (!filter.NumberOfSeats.HasValue || rental.Car.NumberOfSeats == filter.NumberOfSeats) &&
                    (!filter.InsuranceIncluded.HasValue || rental.Car.InsuranceIncluded == filter.InsuranceIncluded) &&
                    (string.IsNullOrEmpty(filter.PickUpCity) || rental.CarAgency.City.Name.Contains(filter.PickUpCity, StringComparison.OrdinalIgnoreCase))
                ).ToList();

            return _mapper.Map<IEnumerable<CarRentalViewDTO>>(filteredRentals);
        
        }

    }
}
