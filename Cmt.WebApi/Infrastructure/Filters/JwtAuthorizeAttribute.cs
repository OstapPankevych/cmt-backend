using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Cmt.WebApi.Infrastructure.Filters
{
    public class JwtAuthorizeAttribute: AuthorizeAttribute
    {
        public new string AuthenticationSchemes => AuthenticationSchemes;

        public JwtAuthorizeAttribute()
            : this(null)
        { }

        public JwtAuthorizeAttribute(string policy)
            : base(policy)
        {
            base.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
