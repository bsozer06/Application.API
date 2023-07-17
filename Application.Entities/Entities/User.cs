using Application.Entities;

namespace Entities
{
    public class User: IEntitiy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Token { get; set; }
    }
}