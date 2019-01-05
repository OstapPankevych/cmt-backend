namespace Cmt.Bll.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public JwtDto Jwt { get; set; }
    }
}
