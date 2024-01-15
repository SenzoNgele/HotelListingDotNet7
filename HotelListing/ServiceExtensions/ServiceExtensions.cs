using HotelListing.Data.Database;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection service)
        {
            var builder = service.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), service);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
    }
}
