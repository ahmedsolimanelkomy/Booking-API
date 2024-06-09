using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class PassportDto
    {
           
            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            [Required, MinLength(14), MaxLength(14)]
            public string? NationalId { get; set; }

            [Required, StringLength(20)]
            public string? PassportNumber { get; set; }

            public string? IssuingCountry { get; set; }

            public DateOnly ExpiryDate { get; set; }

            public string? UserId { get; set; }
        }
    }


