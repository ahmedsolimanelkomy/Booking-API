using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class CarRental
    {
        public int Id { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be a positive value")]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        [Required]
        public double PickUpLocation { get; set; }

        [Required]
        public double DropOffLocation { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        public DateTime DropOffDate { get; set; }

        //foreign keys 
        public ICollection<Car>? Cars { get; set; } = new HashSet<Car>();

        public CarRentalInvoice? CarRentalInvoice { get; set; }
    }


}
