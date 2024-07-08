using Booking_API.DTOs;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface ICarRentalService : IService<CarRental>
    {
        Task<GeneralResponse<CreateCarRentDTO>> CreateCarRentAsync(CreateCarRentDTO rentDto);
        Task<GeneralResponse<CarRentalInvoice>> CreateInvoiceAsync(CreateCarRentDTO rentDto, decimal amount, int userId, PaymentMethod paymentMethod);
        Task UpdateCarRentalStatusAsync();
        public Task<IEnumerable<CarRentalViewDTO>> GetFilteredCarRentals(CarRentalFilterationDTO filter);
        public Task<IEnumerable<CarRentalViewDTO>> GetFilteredUserCarRents(UserCarRentalFilterDTO filter);

    }
}
