namespace Cmt.Bll.DTOs.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public JwtDto Jwt { get; set; }
    }
}
