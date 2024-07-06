using Booking_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.DTOs
{
    public class PassportDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required, MinLength(14), MaxLength(14)]
        public string NationalId { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string PassportNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string? IssuingCountry { get; set; }

        public DateOnly ExpiryDate { get; set; }

    }
}


