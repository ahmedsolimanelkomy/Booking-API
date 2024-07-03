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

        public async Task<IEnumerable<CarRental>> GetFilteredCarRentalsAsync(CarRentalFilterationDTO filter)
        {
            var carRentals = await _unitOfWork.CarRental.GetAllAsync(new[] { "Car", "Car.CarAgency", "Car.CarAgency.City" });

            //var filteredCarRentals = carRentals.Where(carRental =>
            //    (string.IsNullOrEmpty(filter.PickUpCity) || carRental.Car.CarAgency.City.Name.Contains(filter.PickUpCity, StringComparison.OrdinalIgnoreCase)) &&
            //    (!filter.PickUpDate.HasValue || carRental.PickUpDate == filter.PickUpDate.Value) &&
            //    (!filter.DropOffDate.HasValue || carRental.DropOffDate == filter.DropOffDate.Value) &&
            //    (!filter.Price.HasValue || carRental.TotalPrice <= filter.Price.Value) &&
            //    (!filter.GearType.HasValue || carRental?.Car?.GearType == filter.GearType.Value) &&
            //    (!filter.ModelOfYear.HasValue || carRental?.Car?.ModelOfYear == filter.ModelOfYear.Value) &&
            //    (string.IsNullOrEmpty(filter.Brand) || carRental.Car.Brand.Contains(filter.Brand, StringComparison.OrdinalIgnoreCase)) &&
            //    (!filter.InsuranceIncluded.HasValue || carRental?.Car?.InsuranceIncluded == filter.InsuranceIncluded.Value) &&
            //    (!filter.NumberOfSeats.HasValue || carRental?.Car?.NumberOfSeats == filter.NumberOfSeats.Value) &&
            //    (string.IsNullOrEmpty(filter.AgencyName) || carRental.Car.CarAgency.Name.Contains(filter.AgencyName, StringComparison.OrdinalIgnoreCase))
            //).ToList();

            return /*filteredCarRentals*/ carRentals;
        }

    }
}
