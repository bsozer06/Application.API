using Application.DTOs;
using Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet(nameof(GetUsers))]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            
            if (users == null) return NotFound(new { message = "There are not registered users in the system" });
            
            return Ok(users);
        }

        [Authorize]
        [HttpGet(nameof(GetUseByUserId))]
        public IActionResult GetUseByUserId(int userId)
        {
            var user = _userService.GetUser(userId);
            
            if (user == null) return NotFound(new { message = "This user cannot be found!" });
            
            return Ok(user);
        }

        [Authorize]
        [HttpGet(nameof(GetUserByUserName))]
        public IActionResult GetUserByUserName(string userName)
        {
            var user = _userService.GetUser(userName);

            if (user == null) return NotFound(new { message = "This user cannot be found!" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.UserName, loginDto.Password);
            
            if (user == null) return BadRequest(new { message = "Username or password is wrong!" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var isUserExist = _userService.IsUserExist(userDto);
            
            if (isUserExist) return BadRequest("The username is available!");
            
            _userService.AddUser(userDto);

            return Ok(userDto);   
        }
    }
}
