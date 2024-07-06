using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Passport
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [MinLength(14), MaxLength(14), DisplayName("National ID")]
        public string? NationalId { get; set; }

        [StringLength(20)]
        public string? PassportNumber { get; set; }

        [StringLength(50)]
        public string? IssuingCountry { get; set; }

        public DateOnly ExpiryDate { get; set; }

        // Foreign keys
        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }

        // Navigation Properties
        public ApplicationUser? User { get; set; }
    }
}
