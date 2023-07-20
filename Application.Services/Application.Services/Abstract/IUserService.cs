using Application.DTOs;

namespace Application.Services.Abstract
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        IEnumerable<UserDto> GetUsers();
        void AddUser(UserDto userDto);
        bool IsUserExist(UserDto userDto);
    }
}
