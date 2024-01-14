using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data.Entities
{
    public class Hotel : BaseEntity
    {
        public string Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
