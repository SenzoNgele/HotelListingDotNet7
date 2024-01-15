using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManage, /*SignInManager<ApiUser> signInManager*/ ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManage;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;

                var result =  await _userManager.CreateAsync(user).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Accepted();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong in the {nameof(RegisterAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        //{
        //    _logger.LogInformation($"Login attempt for {userLoginDTO.Email}");
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userLoginDTO.Email, userLoginDTO.Password, false, false).ConfigureAwait(false);

        //        if (!result.Succeeded)
        //        {
        //            return Unauthorized(userLoginDTO);
        //        }

        //        return Accepted();
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError(exception, $"Something went wrong in the {nameof(LoginAsync)}");
        //        return StatusCode(500, "Internal Server Error. Please Try Again Later");
        //    }
        //}
    }
}
