using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Data.IUnit;
using HotelListing.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        {
            try
            {
                var contries = await _unitOfWork.CountriesRepo.GetAll().ConfigureAwait(false);
                var result = _mapper.Map<IList<CountryDTO>>(contries);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in the {nameof(GetCountriesAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
