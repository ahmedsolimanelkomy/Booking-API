using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Booking_API.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string? Name { get; set; }

        //Foreign Keys
        //Navigation Properties
        public ICollection<Hotel>? Hotels { get; set; } = new HashSet<Hotel>();

    }
}
