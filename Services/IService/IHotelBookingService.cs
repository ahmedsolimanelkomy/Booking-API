using System.Collections.Generic;
using System.Threading.Tasks;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;
using Braintree;

namespace Booking_API.Services.IService
{
    public interface IHotelBookingService : IService<HotelBooking>
    {
        Task<IEnumerable<HotelBookingViewDTO>> GetFilteredBookingsAsync(HotelBookingFilterDTO filter);
        Task<GeneralResponse<CreateHotelBookingDTO>> CreateHotelBookingAsync(CreateHotelBookingDTO bookingDto);
        Task<GeneralResponse<HotelBookingInvoice>> CreateInvoiceAsync(CreateHotelBookingDTO bookingDto, decimal amount, int userId, Models.PaymentMethod paymentMethod);


    }
}
