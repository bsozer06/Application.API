﻿using Application.DTOs;
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

        public IEnumerable<UserDto> AddUser(UserDto userDto)
        {
            _users.Add(userDto);
            return _users;
        }

        public UserDto Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null)
                return null;

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

        public bool IsUserExist(UserDto userDto)
        {
            var userName = userDto.UserName.ToLower();
            return _users.Any(u => u.UserName == userName);
        }
    }
}
