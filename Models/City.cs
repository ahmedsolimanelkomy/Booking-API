using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public ICollection<Hotel>? Hotels { get; set; } = new HashSet<Hotel>();
        public ICollection<CarAgency>? CarAgencies { get; set; } = new HashSet<CarAgency>();
        
    }
}
