using AutoMapper;
using HotelListing.Data.IUnit;
using HotelListing.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelsAsync()
        {
            try
            {
                var hotels = await _unitOfWork.HotelsRepo.GetAll().ConfigureAwait(false);
                var result = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in the {nameof(GetHotelsAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotelAsync(int id)
        {
            try
            {
                var hotel = await _unitOfWork.HotelsRepo.Get(q => q.Id == id, new List<string> { "Country" }).ConfigureAwait(false);
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in the {nameof(GetHotelAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
