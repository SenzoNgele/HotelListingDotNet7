using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.Models
{
    /// <summary>
    /// The user with tranfer data here.
    /// Data from here will be tranfered to Country Entity
    /// The class doesnt know about the database.
    /// Only Country Entity that know about the database
    /// NB: We have two DTO because when we create the country we dont need ID, summary is we can have DTO for CRUD
    /// </summary>

    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Name is too long")]
        public string ShortName { get; set; }
    }

    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }
}
