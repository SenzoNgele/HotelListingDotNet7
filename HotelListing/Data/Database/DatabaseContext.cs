using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "South Africa",
                    ShortName = "SA"
                },
                new Country
                {
                    Id = 2,
                    Name = "Zimbambwe",
                    ShortName = "ZM"
                },
                new Country
                {
                    Id = 3,
                    Name = "Unitesd State",
                    ShortName = "US"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Protea Hotel",
                    Address = "1 street Midrand 1686",
                    CountryId = 1,
                    Rating = 4.2
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Five Start Hotel",
                    Address = "20 Cape Town 5710",
                    CountryId = 1,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Sandals Resort and Spa",
                    Address = "3 bulawayo 1111",
                    CountryId = 2,
                    Rating = 3.2
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
