using HotelListing.Data.Database;
using HotelListing.Data.IRepository;
using HotelListing.Data.IUnit;
using HotelListing.Data.Repository;

namespace HotelListing.Data.Unity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Country> _countriesRepo;
        private IGenericRepository<Hotel> _hotelsRepo;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Country> CountriesRepo => _countriesRepo ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> HotelsRepo => _hotelsRepo ??= new GenericRepository<Hotel>(_context);

        // When oparations are done free up memory (gabage collector)
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
