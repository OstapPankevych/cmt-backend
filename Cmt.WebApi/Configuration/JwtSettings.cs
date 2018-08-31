using System;
namespace Cmt.WebApi.Configuration
{
    public class JwtSettings
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public int UserExpirationTimeMinutes { get; set; }
    }
}
