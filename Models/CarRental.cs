using Booking_API.Validations;
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

        [CheckInDate]
        public DateTime PickUpDate { get; set; }

        [CheckOutDate("PickUpDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime DropOffDate { get; set; }


        //Foreign Keys
        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }
        [ForeignKey("CarRentalInvoice")]
        public int? CarRentalInvoiceId { get; set; }
        [ForeignKey("CarAgency")]
        public int? CarAgencyId { get; set; }
        
        [ForeignKey("Car")]
        public int? CarId { get; set; }

        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }
        public Car? Car { get; set; }
        public CarAgency? CarAgency { get; set; }
        public CarRentalInvoice? CarRentalInvoice { get; set; }
    }


}
