using System;
namespace Cmt.WebApi.Configuration
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public int UserExpirationTimeMinutes { get; set; }
    }
}
