using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum RentalStatus
    {
        Booked,
        InProgress,
        Completed,
        Canceled
    }

    public class CarRental
    {
        [Key]
        public int Id { get; set; }
        public DateTime RentalDate { get; set; }
        public RentalStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }

        [ForeignKey("Car")]
        public int CarID { get; set; }
        [ForeignKey("User")]
        public int ApplicationUserID { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceID { get; set; }
        [ForeignKey("Agency")]
        public int AgencyID { get; set; }

        public Car Car { get; set; }
        public ApplicationUser User { get; set; }
        public CarRentalInvoice Invoice { get; set; }
        public CarAgency Agency { get; set; }

        // Remove the redundant CarRentalInvoiceId and CarRentalInvoice properties
    }
}
