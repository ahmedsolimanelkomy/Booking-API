﻿using AutoMapper;
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
        public CarService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilteredCarDTO>> GetCarByBrand(string Brand)
        {
            var cars = await _unitOfWork.Cars.GetAllAsync(new[] { "CarPhotos" });

            var filteredCars = cars
                .Where(cars => cars.Brand != null && cars.Brand.Equals(Brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(filteredCars);
        }

        public async Task<IEnumerable<FilteredCarDTO>> GetFilteredCars(CarRentalFilterationDTO filter)
        {

            var cars = await _unitOfWork.Cars.GetListAsync(car => true, // No initial filter
                new[] { "CarAgency", "CarAgency.City", "CarPhotos" });

            // Apply the filters
            var filteredCars = cars.Where(car =>
                (!filter.CityId.HasValue ||
                 (car.CarAgency?.CityId != null && car.CarAgency.CityId == filter.CityId.Value)) &&
                (!filter.MinPrice.HasValue || (car.RentPrice.HasValue && car.RentPrice.Value >= filter.MinPrice.Value)) &&
                (!filter.MaxPrice.HasValue || (car.RentPrice.HasValue && car.RentPrice.Value <= filter.MaxPrice.Value)) &&
                (!filter.GearType.HasValue || car.GearType == filter.GearType) &&
                (!filter.ModelOfYear.HasValue || car.ModelOfYear == filter.ModelOfYear) &&
                (string.IsNullOrEmpty(filter.Brand) ||
                 (car.Brand != null && car.Brand.Equals(filter.Brand, StringComparison.OrdinalIgnoreCase))) &&
                (!filter.InsuranceIncluded.HasValue || car.InsuranceIncluded == filter.InsuranceIncluded) &&
                (!filter.NumberOfSeats.HasValue || car.NumberOfSeats == filter.NumberOfSeats) &&
                (!filter.AgencyId.HasValue ||
                 (car.CarAgency?.Id != null && car.CarAgency.Id == filter.AgencyId.Value))
            ).ToList();

            // Map the filtered cars to FilteredCarDTO
            return _mapper.Map<IEnumerable<FilteredCarDTO>>(filteredCars);
        }
    }

}

