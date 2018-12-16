using System;

namespace Cmt.Bll.DTOs.Users
{
    public class JwtDto
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
