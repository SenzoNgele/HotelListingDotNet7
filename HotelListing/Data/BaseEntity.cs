using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
    }
}
