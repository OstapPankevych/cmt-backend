namespace Cmt.WebApi.Infrastructure.Configuration
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public int UserExpirationTimeMinutes { get; set; }
    }
}
