using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarRental
{
    public class CarRentalViewDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal TotalPrice { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        public DateTime DropOffDate { get; set; }

        [Required]
        [StringLength(50)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(100)]
        public string CarAgencyName { get; set; }

        public string Brand { get; set; }

        public int UserId { get; set; }

        public int CarAgencyId { get; set; }

        public ICollection<CarPhoto> carPhotos { get; set; }

        public CarRentalInvoice CarRentalInvoice { get; set; }
    }
}
