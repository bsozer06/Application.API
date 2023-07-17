using Application.DTOs;
using Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.UserName, userDto.Password);
            if (user == null)
                return BadRequest(new { message = "Kullanici veya şifre hatalı!" });

            return Ok(user);
        }
    }
}
