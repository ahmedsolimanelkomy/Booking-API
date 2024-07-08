using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Booking_API.Services
{
    public class CarRentalService : Service<CarRental>, ICarRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarService _carService;
        private readonly ICarAgencyService _carAgencyService;
        private readonly ICarRentalInvoiceService _carRentalInvoiceService;

        public CarRentalService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, ICarService carService, ICarAgencyService carAgencyService, ICarRentalInvoiceService carRentalInvoiceService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _carService = carService;
            _carAgencyService = carAgencyService;
            _carRentalInvoiceService = carRentalInvoiceService;
        }

        public async Task<IEnumerable<CarRentalViewDTO>> GetFilteredCarRentals(CarRentalFilterationDTO filter)
        {
            var rentals = await _unitOfWork.CarRental.GetAllAsync(new[] { "Car", "CarAgency", "Car.CarPhotos", "ApplicationUser", "CarRentalInvoice" });

            var filteredRentals = rentals
                .Where(rental =>
                    (!filter.AgencyId.HasValue || rental.CarAgency?.Id == filter.AgencyId.Value) &&
                    (!filter.PickUpDate.HasValue || rental.PickUpDate >= filter.PickUpDate.Value) &&
                    (!filter.DropOffDate.HasValue || rental.DropOffDate <= filter.DropOffDate.Value) &&
                    (!filter.GearType.HasValue || rental.Car?.GearType == filter.GearType) &&
                    (!filter.ModelOfYear.HasValue || rental.Car?.ModelOfYear == filter.ModelOfYear) &&
                    (string.IsNullOrEmpty(filter.Brand) || rental.Car?.Brand?.Contains(filter.Brand, StringComparison.OrdinalIgnoreCase) == true) &&
                    (!filter.NumberOfSeats.HasValue || rental.Car?.NumberOfSeats == filter.NumberOfSeats) &&
                    (!filter.InsuranceIncluded.HasValue || rental.Car?.InsuranceIncluded == filter.InsuranceIncluded) &&
                    (!filter.CityId.HasValue || rental.CarAgency?.CityId == filter.CityId.Value)
                ).ToList();

            return _mapper.Map<IEnumerable<CarRentalViewDTO>>(filteredRentals);

        }


        public async Task<IEnumerable<CarRentalViewDTO>> GetFilteredUserCarRents(UserCarRentalFilterDTO filter)
        {
            var rentals = await _unitOfWork.CarRental.GetAllAsync(["Car", "CarAgency", "CarRentalInvoice"]);

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


        public async Task<GeneralResponse<CreateCarRentDTO>> CreateCarRentAsync(CreateCarRentDTO rentDto)
        {
            var user = await _userManager.FindByIdAsync(rentDto.UserId.ToString());
            if (user == null)
            {
                return new GeneralResponse<CreateCarRentDTO>(false, "User not found", rentDto);
            }

            var car = await _carService.GetAsync(c => c.Id == rentDto.CarId);
            if (car == null)
            {
                return new GeneralResponse<CreateCarRentDTO>(false, "Car not found", rentDto);
            }

            var agency = await _carAgencyService.GetAsync(a => a.Id == rentDto.CarAgencyId);
            if (agency == null)
            {
                return new GeneralResponse<CreateCarRentDTO>(false, "Car agency not found", rentDto);
            }

            var booking = _mapper.Map<CarRental>(rentDto);
            booking.Status = BookingStatus.Confirmed;

            try
            {
                await _unitOfWork.CarRental.AddAsync(booking);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CreateCarRentDTO>(false, "Failed to create car rental", rentDto);
            }

            var createdRentDto = _mapper.Map<CreateCarRentDTO>(booking);
            createdRentDto.Id = booking.Id;

            return new GeneralResponse<CreateCarRentDTO>(true, "Car rental added successfully", createdRentDto);
        }

        public async Task<GeneralResponse<CarRentalInvoice>> CreateInvoiceAsync(CreateCarRentDTO rentDto, decimal amount, int userId, PaymentMethod paymentMethod)
        {
            var invoice = new CarRentalInvoice
            {
                Number = GenerateRandomTransactionNumber(),
                Date = DateTime.Now,
                Amount = amount,
                PaymentStatus = PaymentStatus.Completed,
                TransactionId = GenerateRandomTransactionId(),
                PaymentMethod = paymentMethod,
                UserId = userId,
                CarRentalId = rentDto.Id
            };

            try
            {
                await _carRentalInvoiceService.AddAsync(invoice);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<CarRentalInvoice>(false, "Failed to create invoice", null);
            }

            return new GeneralResponse<CarRentalInvoice>(true, "Invoice created successfully", invoice);
        }

        public async Task UpdateCarRentalStatusAsync()
        {
            var carRentals = await _unitOfWork.CarRental.GetAllAsync();

            foreach (var carRental in carRentals)
            {
                if (carRental.DropOffDate < DateTime.Now && carRental.Status != BookingStatus.Completed)
                {
                    carRental.Status = BookingStatus.Completed;

                    try
                    {
                        await _unitOfWork.CarRental.UpdateAsync(carRental);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log or throw)
                    }
                }
            }
        }

        private int GenerateRandomTransactionNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999); // Generates a random number between 100000 and 999999
        }

        private string GenerateRandomTransactionId()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] stringChars = new char[10];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars); // Generates a random alphanumeric string of length 10
        }


    }
}
