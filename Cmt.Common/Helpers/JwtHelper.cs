using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cmt.Common.Helpers
{
    public static class JwtHelper
    {
        public static SecurityKey GetSecurityKey(string jwtKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
        }
    }
}
