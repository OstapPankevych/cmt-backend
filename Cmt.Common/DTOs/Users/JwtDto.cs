using System;
namespace Cmt.Common.DTOs.Users
{
    public class JwtDto
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
