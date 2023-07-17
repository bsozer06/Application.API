using Application.DTOs;

namespace Application.Services.Mock
{
    public static class MockData
    {
        public static List<UserDto> GetMockUsers()
        {
            return new List<UserDto>()
            {
                new UserDto { Id = 1, Name = "Burak", Surname = "Coskun", UserName = "burakc34", Password = "1234" },
                new UserDto { Id = 2, Name = "Deniz", Surname = "Erdem", UserName = "deniz06", Password = "4321" },
                new UserDto { Id = 3, Name = "Burhan", Surname = "Sozer", UserName = "bsozer06", Password = "123456" }
            };
        }
    }
}
