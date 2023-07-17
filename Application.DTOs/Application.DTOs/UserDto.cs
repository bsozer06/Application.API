namespace Application.DTOs
{
    public class UserDto: IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

    }
}