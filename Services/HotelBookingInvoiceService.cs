using AutoMapper;
using Booking_API.DTOs.HotelBookingInvoice;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Services
{
    public class HotelBookingInvoiceService : Service<HotelBookingInvoice>, IHotelBookingInvoiceService
    {
        private readonly IMapper _mapper;

        public HotelBookingInvoiceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelBookingInvoiceDTO>> GetAllInvoicesAsync()
        {
            var invoices = await GetAllAsync();
            return _mapper.Map<IEnumerable<HotelBookingInvoiceDTO>>(invoices);
        }

        public async Task<HotelBookingInvoiceDTO> GetInvoiceByIdAsync(int id)
        {
            var invoice = await GetAsync(i => i.Id == id);
            return _mapper.Map<HotelBookingInvoiceDTO>(invoice);
        }

        public async Task<HotelBookingInvoiceDTO> CreateInvoiceAsync(HotelBookingInvoiceDTO invoiceDTO)
        {
            var invoice = _mapper.Map<HotelBookingInvoice>(invoiceDTO);
            await AddAsync(invoice);
            return _mapper.Map<HotelBookingInvoiceDTO>(invoice);
        }

        public async Task<HotelBookingInvoiceDTO> UpdateInvoiceAsync(int id, HotelBookingInvoiceDTO invoiceDTO)
        {
            var existingInvoice = await GetAsync(i => i.Id == id);
            if (existingInvoice == null)
                return null;

            _mapper.Map(invoiceDTO, existingInvoice);
            await UpdateAsync(existingInvoice);
            return _mapper.Map<HotelBookingInvoiceDTO>(existingInvoice);
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var existingInvoice = await GetAsync(i => i.Id == id);
            if (existingInvoice == null)
                return false;

            await DeleteAsync(id);
            return true;
        }
    }
}
