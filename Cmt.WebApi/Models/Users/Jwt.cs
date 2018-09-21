using System;
namespace Cmt.WebApi.Models.Users
{
    public class Jwt
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
