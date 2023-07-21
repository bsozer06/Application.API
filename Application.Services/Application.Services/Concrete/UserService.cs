using Application.DTOs;
using Application.Services.Abstract;
using Application.Services.Helpers;
using Application.Services.Mock;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly List<UserDto> _users;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _users = MockData.GetMockUsers();
        }

        public void AddUser(UserDto userDto)
        {
            if (userDto == null) return;

            _users.Add(userDto);
        }

        public void RemoveUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            
            if (user == null) return;

            _users.Remove(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            if (userDto == null) return;

            var user = _users.FirstOrDefault(u => u.Id == userDto.Id);

            user.UserName = userDto?.UserName ?? user.UserName;
            user.Surname = userDto?.Surname ?? user.Surname;
            user.Name = userDto?.Name ?? user.Name;   
            user.Id = userDto?.Id ?? user.Id;
            user.Password = userDto?.Password ?? user.Password;
        }

        public UserDto Authenticate(string username, string password)
        {
            if (username == null || password == null) return null;
            
            var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null) return null;

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(createdToken);

            user.Password = null;

            return user;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _users.Select(u =>
            {
                u.Password = null;
                return u;
            });
        }

        public UserDto GetUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            
            if (user == null) return null;

            return user;
        }

        public UserDto GetUser(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            if (user == null) return null;

            return user;
        }

        public bool IsUserExist(UserDto userDto)
        {
            var userName = userDto.UserName.ToLower();
            return _users.Any(u => u.UserName == userName);
        }
    }
}
