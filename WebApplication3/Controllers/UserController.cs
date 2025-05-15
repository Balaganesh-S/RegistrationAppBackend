using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]

        public async Task<IActionResult> registerUser([FromBody] RegistrationRequestDto registrationRequestDto)
        {
             var response = await _userService.RegisterUser(registrationRequestDto);
             return StatusCode(response.StatusCode, response);

        }
    }
}
