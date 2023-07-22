using Application.DTOs;

namespace Application.Services.Abstract
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        IEnumerable<UserDto> GetUsers();
        UserDto GetUser(int userId);
        UserDto GetUser(string userName);
        void AddUser(UserDto userDto);
        void RemoveUser(int userId);
        void UpdateUser(UserDto userDto);
        bool IsUserExist(UserDto userDto);
    }
}
