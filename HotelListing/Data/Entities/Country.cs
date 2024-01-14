namespace HotelListing.Data.Entities
{
    public class Country : BaseEntity
    {
        public string ShortName { get; set; }

        // it gives us all the hotels in that country NB: It doesnt need migration it wont go in the db
        public virtual IList<Hotel> Hotels { get; set; } 
    }
}
