namespace Cmt.WebApi.Models.Users
{
    public class UserResponse
    {
        public User User { get; set; }
        public Jwt JwtToken { get; set; }
    }
}
