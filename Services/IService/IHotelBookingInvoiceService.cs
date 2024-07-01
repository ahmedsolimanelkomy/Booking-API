using Booking_API.DTOs.HotelBookingInvoice;
using Booking_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Services.IService
{
    public interface IHotelBookingInvoiceService : IService<HotelBookingInvoice>
    {
        Task<IEnumerable<HotelBookingInvoiceDTO>> GetAllInvoicesAsync();
        Task<HotelBookingInvoiceDTO> GetInvoiceByIdAsync(int id);
        Task<HotelBookingInvoiceDTO> CreateInvoiceAsync(HotelBookingInvoiceDTO invoiceDTO);
        Task<HotelBookingInvoiceDTO> UpdateInvoiceAsync(int id, HotelBookingInvoiceDTO invoiceDTO);
        Task<bool> DeleteInvoiceAsync(int id);
    }
}
