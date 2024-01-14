using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.Models
{
    /// <summary>
    /// The user with tranfer data here.
    /// Data from here will be tranfered to Hotel Entity
    /// The class doesnt know about the database.
    /// Only Hotel Entity that know about the database
    /// NB: We have two DTO because when we create the Hotel we dont need ID, summary is we can have DTO for CRUD
    /// </summary>
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Hotel name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Address is too long")]
        public string Address { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }

    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; } 
    }
}
