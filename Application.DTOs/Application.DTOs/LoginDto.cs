namespace Application.DTOs
{
    public class LoginDto: IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
