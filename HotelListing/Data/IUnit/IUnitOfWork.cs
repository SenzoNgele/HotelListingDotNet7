using HotelListing.Data.IRepository;

namespace HotelListing.Data.IUnit
{
    public interface IUnitOfWork : IDisposable 
    {
        IGenericRepository<Country> CountriesRepo { get; }
        IGenericRepository<Hotel> HotelsRepo { get; }
        Task Save();
    }
}
