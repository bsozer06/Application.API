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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userService.GetUsers();
            
            if (users == null) return NotFound(new { message = "This user cannot be found!" });
            
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.UserName, loginDto.Password);
            
            if (user == null) return BadRequest(new { message = "Username or password is wrong!" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var isUserExist = _userService.IsUserExist(userDto);
            
            if (isUserExist) return BadRequest("The username is available!");
            
            _userService.AddUser(userDto);

            return Ok(userDto);   
        }
    }
}
