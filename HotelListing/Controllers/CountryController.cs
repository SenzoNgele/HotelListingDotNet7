using HotelListing.Data.IUnit;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        {
            try
            {
                var contries = await _unitOfWork.CountriesRepo.GetAll().ConfigureAwait(false);
                return Ok(contries);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in the {nameof(GetCountriesAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
